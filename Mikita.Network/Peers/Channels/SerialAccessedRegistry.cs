using Mikita.Network.Peers.Keys;
using System.Net;
using System.Threading;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Serializes every open, lookup, and release of a
/// peer channel registry so sharing and reference
/// counting stay atomic under concurrent use.
/// </summary>
public sealed class SerialAccessedRegistry
	(
		IPeerChannelRegistry source,
		Lock access
	)
	: IPeerChannelRegistry
	{
		public IPeerChannel Open(IPEndPoint endpoint)
			{
				lock (access) return Wrap(source.Open(endpoint));
			}

		public IPeerChannel? With(PeerChannelId id)
			{
				lock (access)
					{
						var channel = source.With(id);
						return channel is null ? null : Wrap(channel);
					}
			}

		private IPeerChannel Wrap(IPeerChannel channel)
			=> new SerialAccessedChannel(channel, access);

		private sealed class SerialAccessedChannel
			(
				IPeerChannel channel,
				Lock access
			)
			: IPeerChannel
			{
				public PeerChannelId Id
					=> channel.Id;

				public PeerKey Key
					=> channel.Key;

				public PeerChannelState State
					=> channel.State;

				public void Dispose()
					{
						lock (access) channel.Dispose();
					}
			}
	}