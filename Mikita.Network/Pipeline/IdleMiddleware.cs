using Mikita.Network.Peers.Keys;
using System;

namespace Mikita.Network.Pipeline;

public sealed class IdleMiddleware
	: IMiddleware
	{
		public void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			)
			{}
	}