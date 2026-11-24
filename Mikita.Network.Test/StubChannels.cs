using Mikita.Network.Peers.Channels;
using Mikita.Network.Peers.Keys;
using System.Net;

namespace Mikita.Network.Test;

/// <summary>
/// Produces distinct in-memory peer channels so channel
/// registry tests can observe creation, sharing, and
/// disposal without real network peers.
/// </summary>
internal sealed class StubChannelSource: IPeerChannelSource
	{
		public int Created { get; private set; }

		public IPEndPoint? Requested { get; private set; }

		public StubChannel Latest { get; private set; } = null!;

		public IPeerChannel Open(IPEndPoint endpoint)
			{
				Requested = endpoint;
				Created++;
				Latest = new StubChannel(new PeerChannelId(Created));
				return Latest;
			}
	}

internal sealed class StubChannel
	(
		PeerChannelId id
	)
	: IPeerChannel
	{
		public PeerChannelId Id { get; }
			= id;

		public PeerKey Key { get; }
			= new(id.Number);

		public PeerChannelState State { get; private set; }
			= PeerChannelState.Offline;

		public void Dispose()
			{
				State = PeerChannelState.Closed;
			}
	}