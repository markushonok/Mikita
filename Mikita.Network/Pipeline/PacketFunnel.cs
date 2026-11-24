using LiteNetLib;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using System;
using System.Net;

namespace Mikita.Network.Pipeline;

/// <summary>
/// Resolves a received packet to the key of the transport that
/// carried it. The sender's endpoint key or connection key
/// enters the pipeline unchanged; associating an incoming
/// transport with a session is the response router's job.
/// </summary>
public sealed class PacketFunnel
	(
		IPeerRegistry<IPEndPoint> endpoints,
		IPeerRegistry<NetPeer> connections,
		IMiddleware next
	)
	: IPacketFunnel
	{
		public void Handle
			(
				IPEndPoint sender,
				ReadOnlySpan<byte> bytes
			)
			{
				using var key = endpoints.KeyFor(sender);
				next.Handle(key, bytes);
			}

		public void Handle
			(
				NetPeer sender,
				ReadOnlySpan<byte> bytes
			)
			{
				using var key = connections.KeyFor(sender);
				next.Handle(key, bytes);
			}
	}