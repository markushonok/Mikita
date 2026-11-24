using Mikita.Network.Peers.Keys;
using System.Collections.Generic;
using System.Net;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Creates peer channels through a source and keeps each one
/// registered by id until it is disposed.
/// </summary>
public sealed partial class PeerChannelRegistry
	{
		public static IPeerChannelRegistry With
			(
				IPeerChannelSource source
			)
			=> new PeerChannelRegistry
				(
					source,
					channels: new Dictionary<PeerChannelId, IPeerChannel>()
				);
	}

public sealed partial class PeerChannelRegistry
	(
		IPeerChannelSource source,
		IDictionary<PeerChannelId, IPeerChannel> channels
	)
	: IPeerChannelRegistry
	{
		public IPeerChannel Open(IPEndPoint endpoint)
			{
				var channel = new RegisteredChannel
					(
						channels,
						source.Open(endpoint)
					);

				channels.Add(channel.Id, channel);
				return channel;
			}

		public IPeerChannel? With(PeerChannelId id)
			=> channels.TryGetValue(id, out var channel)
				? channel
				: null;

		private sealed class RegisteredChannel
			(
				IDictionary<PeerChannelId, IPeerChannel> channels,
				IPeerChannel channel
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
						try
							{
								channel.Dispose();
							}
						finally
							{
								channels.Remove(channel.Id);
							}
					}
			}
	}