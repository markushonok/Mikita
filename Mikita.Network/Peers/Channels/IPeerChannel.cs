using Mikita.Network.Peers.Keys;
using System;

namespace Mikita.Network.Peers.Channels;

/// <summary>
/// A connection attempt to a peer at
/// a specific endpoint. A channel starts
/// Offline, becomes Online while connected,
/// and is closed by disposal.
/// </summary>
public interface IPeerChannel: IDisposable
	{
		PeerChannelId Id { get; }

		/// <summary>
		/// The process-local key that identifies the remote peer
		/// for message routing. It is stable until the channel is
		/// disposed and is never sent over the wire.
		/// </summary>
		PeerKey Key { get; }

		PeerChannelState State { get; }
	}