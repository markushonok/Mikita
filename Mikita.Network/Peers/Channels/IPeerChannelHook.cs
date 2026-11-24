namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Receives peer channel lifecycle events: <see cref="Open"/>
/// when the channel is created, <see cref="Online"/>
/// when the connection is established,
/// <see cref="Offline"/> when the connection drops,
/// and <see cref="Close"/> when the channel is disposed.
/// </summary>
public interface IPeerChannelHook
	{
		void Open();

		void Online();

		void Offline();

		void Close();
	}