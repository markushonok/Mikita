using LiteNetLib;
using Mikita.Identity;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using Mikita.Network.Pipeline;
using System.Net;

namespace Mikita.Network.Test;

public static class PacketReceiptTest
	{
		[Fact]
		public static void EndpointPacketUsesEndpointKey()
			{
				var keys = PeerKeyPool.New;
				var endpoints = EndpointRegistry(keys);
				var endpoint = Endpoint(60000);
				var key = endpoints.ManualKeyFor(endpoint);
				var received = new KeyCapture();

				Funnel(endpoints, ConnectionRegistry(keys), received)
					.Handle(endpoint, []);

				received.Keys.ShouldBe([key]);
			}

		[Fact]
		public static void ConnectionPacketUsesConnectionKey()
			{
				var keys = PeerKeyPool.New;
				var connections = ConnectionRegistry(keys);
				using var peers = new OutgoingPeers();
				var connection = peers.At(60001);
				var key = connections.ManualKeyFor(connection);
				var received = new KeyCapture();

				Funnel(EndpointRegistry(keys), connections, received)
					.Handle(connection, []);

				received.Keys.ShouldBe([key]);
			}

		[Fact]
		public static void AliasDoesNotRenameIncoming()
			{
				var keys = PeerKeyPool.New;
				var connections = ConnectionRegistry(keys);
				using var peers = new OutgoingPeers();
				var connection = peers.At(60002);
				var key = connections.ManualKeyFor(connection);
				PeerAliasMap.New.Point(new PeerKey(9000), key);
				var received = new KeyCapture();

				Funnel(EndpointRegistry(keys), connections, received)
					.Handle(connection, []);

				received.Keys.ShouldBe([key]);
			}

		[Fact]
		public static void UnknownEndpointEntersWithItsTransportKey()
			{
				var keys = PeerKeyPool.New;
				var received = new KeyCapture();

				Funnel
					(
						EndpointRegistry(keys),
						ConnectionRegistry(keys),
						received
					)
					.Handle(Endpoint(60003), []);

				received.Keys.Count.ShouldBe(1);
			}

		private static PacketFunnel Funnel
			(
				IPeerRegistry<IPEndPoint> endpoints,
				IPeerRegistry<NetPeer> connections,
				IMiddleware next
			)
			=> new(endpoints, connections, next);

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

		private static IPEndPoint Endpoint(int port)
			=> new(IPAddress.Loopback, port);

		private sealed class KeyCapture: IMiddleware
			{
				public List<PeerKey> Keys { get; } = [];

				public void Handle
					(
						PeerKey peer,
						ReadOnlySpan<byte> bytes
					)
					=> Keys.Add(peer);
			}
	}