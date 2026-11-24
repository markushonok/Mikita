using Mikita.Network.Peers.Keys;
using System;

namespace Mikita.Network.Pipeline;

public interface IMiddleware
	{
		void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			);
	}

public interface IMiddleware<in T>
	{
		void Handle
			(
				PeerKey peer,
				T message
			);
	}