using Mikita.Identity;
using Mikita.Network.Peers.Keys;
using System;
using System.Collections.Generic;

namespace Mikita.Network.Peers.Registries;

public sealed partial class PeerRegistry<T>
	where T: class
	{
		public static IPeerRegistry<T> With
			(
				IKeyPool<PeerKey> keys
			)
			=> new PeerRegistry<T>
				(
					keys,
					peers: new Dictionary<PeerKey, T>()
				);
	}

public sealed partial class PeerRegistry<T>
	(
		IKeyPool<PeerKey> keys,
		IDictionary<PeerKey, T> peers
	)
	: IPeerRegistry<T>
	where T: class
	{
		public PeerKey ManualKeyFor
			(
				T peer
			)
			{
				var key = keys.Acquire();
				peers.Add(key, peer);
				return key;
			}

		public T? PeerWith(PeerKey key)
			=> peers.TryGetValue(key, out var peer)
				? peer
				: null;

		public void Release(PeerKey key)
			{
				if (!peers.Remove(key))
					throw UnknownKeyException(key);

				keys.Release(key);
			}

		private static Exception UnknownKeyException(PeerKey key)
			=> new InvalidOperationException
				(
					$"Peer key '{key}' is not registered."
				);
	}