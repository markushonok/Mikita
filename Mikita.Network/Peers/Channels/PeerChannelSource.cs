using LiteNetLib;
using Mikita.Identity;
using Mikita.Logging;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using Mikita.Observation.Events;
using Mikita.Observation.Subscriptions;
using Mikita.Observation.Tables;
using Mikita.Structs.Referring;
using System.Net;
using System.Threading;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Creates LiteNetLib-backed peer channels, assigning each a
/// channel id and a stable peer key, pointing the key at the
/// channel's current connection, and raising connectivity events.
/// </summary>
public sealed partial class PeerChannelSource
	{
		public static IPeerChannelSource With
			(
				EventBasedNetListener listener,
				NetManager manager,
				string protocolKey,
				IKeyPool<PeerKey> keys,
				IPeerRegistry<NetPeer> connections,
				IPeerAliasMap aliases,
				IEventSourceMap<PeerChannelId, IPeerChannelHook> events,
				ILog log
			)
			=> new PeerChannelSource
				(
					listener,
					manager,
					protocolKey,
					keys,
					connections,
					aliases,
					log,
					events,
					PeerChannelIdPool.New
				);
	}

public sealed partial class PeerChannelSource
	(
		EventBasedNetListener listener,
		NetManager manager,
		string protocolKey,
		IKeyPool<PeerKey> keys,
		IPeerRegistry<NetPeer> connections,
		IPeerAliasMap aliases,
		ILog log,
		IEventSourceMap<PeerChannelId, IPeerChannelHook> events,
		IKeyPool<PeerChannelId> ids
	)
	: IPeerChannelSource
	{
		public IPeerChannel Open(IPEndPoint endpoint)
			{
				var id = ids.Acquire();
				var key = AcquireKey(id);

				var state = Ref.To(PeerChannelState.Offline);
				var target = Ref.To(endpoint);
				var connection = Ref<NetPeer>.Null.WithSerialAccess;
				var changes = events.Of(id);

				var hook = new ConnectionHook
					(
						key,
						target,
						connection,
						connections,
						aliases,
						state,
						changes,
						new Lock(),
						isReleased: Ref.To(false)
					);

				var subscription = listener.SubscriptionOf
					(
						hook.Connect,
						hook.Disconnect
					);

				var loop = PeerChannelLoop.With
					(
						listener,
						manager,
						protocolKey,
						() => target.Value,
						connection,
						log
					);

				var channel = new PeerChannel
					(
						id,
						key,
						state,
						changes,
						loop,
						subscription,
						hook,
						keys,
						ids,
						isDisposed: Ref.To(false)
					);

				try
					{
						subscription.Activate();
						loop.Start();
					}
				catch
					{
						channel.Dispose();
						throw;
					}

				return channel;
			}

		private PeerKey AcquireKey(PeerChannelId id)
			{
				try
					{
						return keys.Acquire();
					}
				catch
					{
						ids.Release(id);
						throw;
					}
			}

		private sealed class ConnectionHook
			(
				PeerKey key,
				IRef<IPEndPoint> endpoint,
				IRef<NetPeer?> connection,
				IPeerRegistry<NetPeer> connections,
				IPeerAliasMap aliases,
				IRef<PeerChannelState> state,
				IEventSource<IPeerChannelHook> changes,
				Lock access,
				IRef<bool> isReleased
			)
			{
				public void Connect(NetPeer aroused)
					{
						lock (access)
							{
								if (isReleased.Value) return;

								if
									(
										!ReferenceEquals(aroused, connection.Value)
										&& !aroused.Endpoint.Equals(endpoint.Value)
									)
									return;

								Clear();

								aliases.Point
									(
										key,
										connections.ManualKeyFor(aroused)
									);

								state.SetTo(PeerChannelState.Online);
								changes.Raise(hook => hook.Online());
							}
					}

				public void Disconnect
					(
						NetPeer aroused,
						DisconnectInfo info
					)
					{
						lock (access)
							{
								if (isReleased.Value) return;

								if (!ReferenceEquals(aroused, connection.Value)) return;

								Clear();

								state.SetTo(PeerChannelState.Offline);
								changes.Raise(hook => hook.Offline());
							}
					}

				public void Release()
					{
						lock (access)
							{
								if (isReleased.Value) return;
								isReleased.SetTo(true);

								Clear();
							}
					}

				private void Clear()
					{
						if (aliases.ReferentOf(key) is not PeerKey connectionKey)
							return;

						aliases.Remove(key);
						connections.Release(connectionKey);
					}
			}

		private sealed class PeerChannel
			(
				PeerChannelId id,
				PeerKey key,
				IRef<PeerChannelState> state,
				IEventSource<IPeerChannelHook> changes,
				IPeerChannelLoop loop,
				ISubscription subscription,
				ConnectionHook hook,
				IKeyPool<PeerKey> keys,
				IKeyPool<PeerChannelId> ids,
				IRef<bool> isDisposed
			)
			: IPeerChannel
			{
				public PeerChannelId Id
					=> id;

				public PeerKey Key
					=> key;

				public PeerChannelState State
					=> state.Value;

				public void Dispose()
					{
						if (isDisposed.Value) return;
						isDisposed.Value = true;

						subscription.Deactivate();
						loop.Stop();
						hook.Release();
						keys.Release(key);
						state.SetTo(PeerChannelState.Closed);

						try
							{
								changes.Raise(hook => hook.Close());
							}
						finally
							{
								ids.Release(id);
							}
					}
			}
	}