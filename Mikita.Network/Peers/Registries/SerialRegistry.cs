using Mikita.Network.Peers.Keys;
using System.Threading;

namespace Mikita.Network.Peers.Registries;

public sealed class SerialRegistry<T>
	(
		IPeerRegistry<T> source,
		Lock access
	)
	: IPeerRegistry<T>
	where T: class
	{
		public PeerKey ManualKeyFor(T peer)
			{
				lock (access) return source.ManualKeyFor(peer);
			}

		public T? PeerWith(PeerKey key)
			{
				lock (access) return source.PeerWith(key);
			}

		public void Release(PeerKey key)
			{
				lock (access) source.Release(key);
			}
	}