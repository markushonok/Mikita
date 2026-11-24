using Mikita.Network.Peers.Keys;
using System;
using System.Collections.Generic;

namespace Mikita.Network.Peers.Registries;

public sealed class RefCountedRegistry<T>
	(
		IPeerRegistry<T> source,
		IDictionary<T, PeerKey> keys,
		IDictionary<PeerKey, int> references
	)
	: IPeerRegistry<T>
	where T: class
	{
		public PeerKey ManualKeyFor(T peer)
			=> keys.TryGetValue(peer, out var key)
				? Retain(key)
				: Register(peer);

		private PeerKey Retain(PeerKey key)
			{
				if (!references.TryGetValue(key, out var count))
					throw MissingReferenceCountException(key);

				references[key] = checked(count + 1);
				return key;
			}

		private PeerKey Register(T peer)
			{
				var key = source.ManualKeyFor(peer);
				keys.Add(peer, key);
				references.Add(key, 1);
				return key;
			}

		public T? PeerWith(PeerKey key)
			=> source.PeerWith(key);

		public void Release(PeerKey key)
			{
				if (!references.TryGetValue(key, out var count))
					throw UnknownKeyException(key);

				if (count > 1)
					{
						references[key] = count - 1;
						return;
					}

				ReleaseLast(key);
			}

		private void ReleaseLast(PeerKey key)
			{
				var peer = source.PeerWith(key)
					?? throw MissingPeerException(key);

				if (!keys.Remove(peer))
					throw MissingKeyException(key);

				if (!references.Remove(key))
					throw MissingReferenceCountException(key);

				source.Release(key);
			}

		private static Exception UnknownKeyException(PeerKey key)
			=> new InvalidOperationException
				(
					$"Peer key '{key}' has no reference count."
				);

		private static Exception MissingPeerException(PeerKey key)
			=> new InvalidOperationException
				(
					$"Peer key '{key}' has no registered peer."
				);

		private static Exception MissingKeyException(PeerKey key)
			=> new InvalidOperationException
				(
					$"Peer key '{key}' has no reverse peer mapping."
				);

		private static Exception MissingReferenceCountException(PeerKey key)
			=> new InvalidOperationException
				(
					$"Peer key '{key}' lost its reference count."
				);
	}