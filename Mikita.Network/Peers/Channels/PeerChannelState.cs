namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Connectivity state of a peer channel.
/// </summary>
public enum PeerChannelState
	{
		/// <summary>Not connected; the channel keeps trying to connect.</summary>
		Offline,

		/// <summary>Connected to the peer.</summary>
		Online,

		/// <summary>Permanently stopped by disposal.</summary>
		Closed
	}