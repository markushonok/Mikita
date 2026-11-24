namespace Mikita.Network.Peers.Channels;

/// <summary>
/// Creates peer channels and keeps the ones still open;
/// a channel that is absent from the registry is closed.
/// </summary>
public interface IPeerChannelRegistry
	: IPeerChannelSource
	{
		/// <summary>
		/// The open channel with the given id, or null when
		/// it is not registered and therefore closed.
		/// </summary>
		IPeerChannel? With(PeerChannelId id);
	}