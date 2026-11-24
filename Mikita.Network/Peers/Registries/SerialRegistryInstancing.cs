using System.Threading;

namespace Mikita.Network.Peers.Registries;

public static class SerialRegistryInstancing
	{
		extension<T>(IPeerRegistry<T> registry)
			where T: class
			{
				public IPeerRegistry<T> WithSerialAccess
					=> new SerialRegistry<T>(registry, new Lock());
			}
	}