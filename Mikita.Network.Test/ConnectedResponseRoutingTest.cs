using LiteNetLib;
using Mikita.Identity;
using Mikita.Network.Packets.Senders;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using Mikita.Network.Pipeline;
using System.Net;

namespace Mikita.Network.Test;

public static class ConnectedResponseRoutingTest
	{
		[Fact]
		public static void DelayedResponseRoutesThroughCurrentConnection()
			{
				var keys = PeerKeyPool.New;
				var connections = ConnectionRegistry(keys);
				using var peers = new OutgoingPeers();
				var connection = peers.At(63000);
				var key = connections.ManualKeyFor(connection);
				var peerPackets = new PacketCapture<NetPeer>();
				var defaultPackets = new PacketCapture<PeerKey>();
				var sender = new RoutedPacketSender
					(
						EndpointRegistry(keys),
						connections,
						PeerAliasMap.New,
						new PacketCapture<IPEndPoint>(),
						peerPackets,
						defaultPackets
					);
				var relay = new KeyRelay();
				var funnel = new PacketFunnel
					(
						EndpointRegistry(keys),
						connections,
						relay
					);

				funnel.Handle(connection, []);
				sender.Send(relay.Key, []);

				defaultPackets.Recipients.ShouldBeEmpty();
				peerPackets.Recipients.ShouldBe([connection]);
				relay.Key.ShouldBe(key);
			}

		[Fact]
		public static void ReleasedConnectionIsNoLongerSendable()
			{
				var keys = PeerKeyPool.New;
				var connections = ConnectionRegistry(keys);
				using var peers = new OutgoingPeers();
				var connection = peers.At(63001);
				var key = connections.ManualKeyFor(connection);
				var peerPackets = new PacketCapture<NetPeer>();
				var defaultPackets = new PacketCapture<PeerKey>();
				var sender = new RoutedPacketSender
					(
						EndpointRegistry(keys),
						connections,
						PeerAliasMap.New,
						new PacketCapture<IPEndPoint>(),
						peerPackets,
						defaultPackets
					);

				connections.Release(key);
				sender.Send(key, []);

				defaultPackets.Recipients.ShouldBe([key]);
				peerPackets.Recipients.ShouldBeEmpty();
			}

		private static IPeerRegistry<IPEndPoint> EndpointRegistry
			(
				IKeyPool<PeerKey> keys
			)
			=> PeerRegistry<IPEndPoint>
				.With(keys)
				.WithReferenceCountBy(EndpointKeyComparer.Instance);

		private static IPeerRegistry<NetPeer> ConnectionRegistry
			(
				IKeyPool<PeerKey> keys
			)
			=> PeerRegistry<NetPeer>
				.With(keys)
				.WithReferenceCount;

		private sealed class PacketCapture<T>: IPacketSender<T>
			{
				public List<T> Recipients { get; } = [];

				public void Send(T recipient, ReadOnlySpan<byte> packet)
					=> Recipients.Add(recipient);
			}

		private sealed class KeyRelay: IMiddleware
			{
				public PeerKey Key { get; private set; }

				public void Handle
					(
						PeerKey peer,
						ReadOnlySpan<byte> bytes
					)
					=> Key = peer;
			}
	}