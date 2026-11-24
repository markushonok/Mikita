using System.Net;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Creates peer channels, one per
/// requested endpoint.
/// </summary>
public interface IPeerChannelSource
	{
		IPeerChannel Open(IPEndPoint endpoint);
	}