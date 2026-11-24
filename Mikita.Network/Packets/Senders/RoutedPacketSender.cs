using LiteNetLib;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using System;
using System.Net;

namespace Mikita.Network.Packets.Senders;

/// <summary>
/// Sends to a peer key through the transport that currently
/// carries it. An aliased session key resolves to its current
/// connection key first; the current connection is preferred,
/// then the current endpoint, and finally the default sender
/// when the key has no live transport.
/// </summary>
public sealed class RoutedPacketSender
	(
		IPeerRegistry<IPEndPoint> endpoints,
		IPeerRegistry<NetPeer> connections,
		IPeerAliasMap aliases,
		IPacketSender<IPEndPoint> endpointSender,
		IPacketSender<NetPeer> peerSender,
		IPacketSender<PeerKey> defaultSender
	)
	: IPacketSender<PeerKey>
	{
		public void Send
			(
				PeerKey recipient,
				ReadOnlySpan<byte> packet
			)
			{
				var original = aliases.ReferentOf(recipient) ?? recipient;

				if (connections.PeerWith(original) is NetPeer connection)
					{
						peerSender.Send(connection, packet);
					}
				else if (endpoints.PeerWith(original) is IPEndPoint endpoint)
					{
						endpointSender.Send(endpoint, packet);
					}
				else defaultSender.Send(recipient, packet);
			}
	}