namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Maintains a peer connection: connects
/// to the endpoint and retries after a
/// drop until stopped.
/// </summary>
public interface IPeerChannelLoop
	{
		void Start();

		void Stop();
	}