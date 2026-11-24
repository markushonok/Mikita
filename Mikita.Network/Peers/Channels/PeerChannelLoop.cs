using LiteNetLib;
using Mikita.Logging;
using Mikita.Structs.Referring;
using Mikita.Threading;
using Mikita.Threading.Pools;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Connects and reconnects a LiteNetLib
/// peer to a fixed endpoint until stopped.
/// </summary>
public sealed partial class PeerChannelLoop
	{
		public static IPeerChannelLoop With
			(
				EventBasedNetListener listener,
				NetManager manager,
				string protocolKey,
				Func<IPEndPoint> endpoint,
				IRef<NetPeer?> connection,
				ILog log
			)
			=> new PeerChannelLoop
				(
					listener,
					manager,
					protocolKey,
					endpoint,
					connection,
					TaskPool.NewWith(log),
					new Lock()
				);
	}

public sealed partial class PeerChannelLoop
	(
		EventBasedNetListener listener,
		NetManager manager,
		string protocolKey,
		Func<IPEndPoint> endpoint,
		IRef<NetPeer?> connection,
		ITaskPool tasks,
		Lock access
	)
	: IPeerChannelLoop
	{
		public void Start()
			{
				listener.PeerConnectedEvent += OnConnected;
				listener.PeerDisconnectedEvent += OnDisconnected;
				Connect();
			}

		public void Stop()
			{
				listener.PeerConnectedEvent -= OnConnected;
				listener.PeerDisconnectedEvent -= OnDisconnected;

				lock (access)
					{
						tasks.Stop().Forget();
						connection.Value?.Disconnect();
					}
			}

		private void OnConnected(NetPeer aroused)
			{
				if (aroused.Endpoint.Equals(endpoint()))
					connection.SetTo(aroused);
			}

		private void OnDisconnected
			(
				NetPeer aroused,
				DisconnectInfo info
			)
			{
				if (ReferenceEquals(aroused, connection.Value)) Connect();
			}

		private async Task Retry(CancellationToken cancel)
			{
				await Task.Delay
					(
						TimeSpan.FromMilliseconds(100),
						cancel
					);

				lock (access)
					{
						cancel.ThrowIfRequested();
						Connect();
					}
			}

		private void Connect()
			{
				connection.Value = manager.Connect
					(
						endpoint(),
						protocolKey
					);

				if (connection.Value is null)
					tasks.Put(Retry);
			}
	}