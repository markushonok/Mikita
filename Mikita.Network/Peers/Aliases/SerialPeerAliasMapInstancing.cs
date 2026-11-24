using System.Threading;

namespace Mikita.Network.Peers.Aliases;

public static class SerialPeerAliasMapInstancing
	{
		extension(IPeerAliasMap aliases)
			{
				public IPeerAliasMap WithSerialAccess
					=> new SerialPeerAliasMap(aliases, new Lock());
			}
	}