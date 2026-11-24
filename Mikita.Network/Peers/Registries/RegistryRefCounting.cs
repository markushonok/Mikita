using Mikita.Network.Peers.Keys;
using System.Collections.Generic;

namespace Mikita.Network.Peers.Registries;

public static class RegistryRefCounting
	{
		extension<T>(IPeerRegistry<T> registry)
			where T: class
			{
				public IPeerRegistry<T> WithReferenceCount
					=> registry.WithReferenceCountBy
						(
							EqualityComparer<T>.Default
						);

				public IPeerRegistry<T> WithReferenceCountBy
					(
						IEqualityComparer<T> comparer
					)
					=> new RefCountedRegistry<T>
						(
							registry,
							keys: new Dictionary<T, PeerKey>(comparer),
							references: new Dictionary<PeerKey, int>()
						);
			}
	}