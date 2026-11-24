using Mikita.Network.Peers.Keys;
using Mikita.Nulls;
using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;
using System.Net;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Shares one registered peer channel per endpoint and hands out
/// one reference-counted handle per open or lookup. The channel is
/// disposed when its last handle is.
/// </summary>
public sealed class RefCountedRegistry
	(
		IPeerChannelRegistry source,
		IDictionary<PeerChannelId, IPEndPoint> endpoints,
		IDictionary<IPEndPoint, PeerChannelId> ids,
		IDictionary<PeerChannelId, int> references
	)
	: IPeerChannelRegistry
	{
		public IPeerChannel Open(IPEndPoint endpoint)
			{
				if (ids.TryGetValue(endpoint, out var existing))
					return Retain(source.With(existing).NotNull());

				var channel = source.Open(endpoint);

				endpoints.Add(channel.Id, endpoint);
				ids.Add(endpoint, channel.Id);
				references.Add(channel.Id, 1);

				return new Reference(this, channel, Ref.To(false));
			}

		public IPeerChannel? With(PeerChannelId id)
			{
				var channel = source.With(id);
				return channel is null
					? null
					: Retain(channel);
			}

		private IPeerChannel Retain(IPeerChannel channel)
			{
				var id = channel.Id;

				references[id] = references.TryGetValue(id, out var count)
					? checked(count + 1)
					: throw UnknownChannelException(id);

				return new Reference(this, channel, Ref.To(false));
			}

		private void Release(IPeerChannel channel)
			{
				var id = channel.Id;

				if (!references.TryGetValue(id, out var count))
					throw UnknownChannelException(id);

				if (count > 1)
					{
						references[id] = count - 1;
						return;
					}

				var endpoint = endpoints[id];

				references.Remove(id);
				endpoints.Remove(id);
				ids.Remove(endpoint);
				channel.Dispose();
			}

		private static Exception UnknownChannelException(PeerChannelId id)
			=> new InvalidOperationException
				(
					$"Peer channel '{id}' is not retained."
				);

		private sealed class Reference
			(
				RefCountedRegistry registry,
				IPeerChannel channel,
				IRef<bool> isDisposed
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
						if (isDisposed.Value) return;
						isDisposed.Value = true;

						registry.Release(channel);
					}
			}
	}