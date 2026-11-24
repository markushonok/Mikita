using LiteNetLib;
using System;
using System.Net;

namespace Mikita.Network.Pipeline;

public interface IPacketFunnel
	{
		void Handle
			(
				IPEndPoint sender,
				ReadOnlySpan<byte> bytes
			);

		void Handle
			(
				NetPeer sender,
				ReadOnlySpan<byte> bytes
			);
	}