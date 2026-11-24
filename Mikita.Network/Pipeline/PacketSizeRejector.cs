using Mikita.Network.Peers.Keys;
using System;

namespace Mikita.Network.Pipeline;

public sealed class PacketSizeRejector
	(
		IMiddleware next
	)
	: IMiddleware
	{
		public void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			)
			{
				if (bytes.Length >= sizeof(short))
					next.Handle(peer, bytes);
			}
	}