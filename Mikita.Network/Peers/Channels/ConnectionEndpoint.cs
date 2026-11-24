using LiteNetLib;
using System.Net;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Exposes the endpoint of a LiteNetLib peer.
/// </summary>
public static class ConnectionEndpoint
	{
		extension(NetPeer connection)
			{
				public IPEndPoint Endpoint
					=> new
						(
							connection.Address,
							connection.Port
						);
			}
	}