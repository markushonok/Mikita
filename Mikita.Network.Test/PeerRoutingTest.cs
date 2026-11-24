using LiteNetLib;
using Mikita.Identity;
using Mikita.Network.Packets.Senders;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using System.Net;

namespace Mikita.Network.Test;

public static class PeerRoutingTest
	{
		[Fact]
		public static void EndpointTransportReachesEndpointSender()
			{
				var keys = PeerKeyPool.New;
				var endpoints = EndpointRegistry(keys);
				var endpoint = Endpoint(50000);
				var capture = Capture(endpoints, ConnectionRegistry(keys));

				capture.Sender.Send(endpoints.ManualKeyFor(endpoint), []);

				capture.Endpoints.Recipients.ShouldBe([endpoint]);
				capture.Connections.Recipients.ShouldBeEmpty();
				capture.Default.Recipients.ShouldBeEmpty();
			}

		[Fact]
		public static void AliasedSessionReachesCurrentTransport()
			{
				var keys = PeerKeyPool.New;
				var endpoints = EndpointRegistry(keys);
				var aliases = PeerAliasMap.New;
				var endpoint = Endpoint(50001);
				var session = new PeerKey(9000);
				aliases.Point(session, endpoints.ManualKeyFor(endpoint));
				var capture = Capture
					(
						endpoints,
						ConnectionRegistry(keys),
						aliases
					);

				capture.Sender.Send(session, []);

				capture.Endpoints.Recipients.ShouldBe([endpoint]);
			}

		[Fact]
		public static void ConnectionTransportReachesPeerSender()
			{
				var keys = PeerKeyPool.New;
				var connections = ConnectionRegistry(keys);
				var aliases = PeerAliasMap.New;
				using var peers = new OutgoingPeers();
				var connection = peers.At(50002);
				var session = new PeerKey(9001);
				aliases.Point(session, connections.ManualKeyFor(connection));
				var capture = Capture
					(
						EndpointRegistry(keys),
						connections,
						aliases
					);

				capture.Sender.Send(session, []);

				capture.Connections.Recipients.ShouldBe([connection]);
				capture.Endpoints.Recipients.ShouldBeEmpty();
			}

		[Fact]
		public static void RepointedSessionFollowsCurrentTransport()
			{
				var keys = PeerKeyPool.New;
				var endpoints = EndpointRegistry(keys);
				var connections = ConnectionRegistry(keys);
				var aliases = PeerAliasMap.New;
				var endpoint = Endpoint(50003);
				using var peers = new OutgoingPeers();
				var connection = peers.At(50004);
				var session = new PeerKey(9002);
				var capture = Capture(endpoints, connections, aliases);

				aliases.Point(session, endpoints.ManualKeyFor(endpoint));
				capture.Sender.Send(session, []);

				aliases.Point(session, connections.ManualKeyFor(connection));
				capture.Sender.Send(session, []);

				capture.Endpoints.Recipients.ShouldBe([endpoint]);
				capture.Connections.Recipients.ShouldBe([connection]);
			}

		[Fact]
		public static void UnknownKeyUsesDefaultSender()
			{
				var keys = PeerKeyPool.New;
				var capture = Capture
					(
						EndpointRegistry(keys),
						ConnectionRegistry(keys)
					);

				capture.Sender.Send(new PeerKey(9999), []);

				capture.Default.Recipients.ShouldBe([new PeerKey(9999)]);
				capture.Endpoints.Recipients.ShouldBeEmpty();
				capture.Connections.Recipients.ShouldBeEmpty();
			}

		[Fact]
		public static void RemovedSessionUsesDefaultSender()
			{
				var keys = PeerKeyPool.New;
				var endpoints = EndpointRegistry(keys);
				var aliases = PeerAliasMap.New;
				var session = new PeerKey(9003);
				aliases.Point(session, endpoints.ManualKeyFor(Endpoint(50005)));
				var capture = Capture
					(
						endpoints,
						ConnectionRegistry(keys),
						aliases
					);

				aliases.Remove(session);
				capture.Sender.Send(session, []);

				capture.Default.Recipients.ShouldBe([session]);
				capture.Endpoints.Recipients.ShouldBeEmpty();
			}

		private static RoutedCapture Capture
			(
				IPeerRegistry<IPEndPoint> endpoints,
				IPeerRegistry<NetPeer> connections,
				IPeerAliasMap? aliases = null
			)
			=> new(endpoints, connections, aliases ?? PeerAliasMap.New);

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

		private sealed class RoutedCapture
			{
				public RoutedCapture
					(
						IPeerRegistry<IPEndPoint> endpoints,
						IPeerRegistry<NetPeer> connections,
						IPeerAliasMap aliases
					)
					{
						Endpoints = new PacketCapture<IPEndPoint>();
						Connections = new PacketCapture<NetPeer>();
						Default = new PacketCapture<PeerKey>();
						Sender = new RoutedPacketSender
							(
								endpoints,
								connections,
								aliases,
								Endpoints,
								Connections,
								Default
							);
					}

				public PacketCapture<IPEndPoint> Endpoints { get; }

				public PacketCapture<NetPeer> Connections { get; }

				public PacketCapture<PeerKey> Default { get; }

				public RoutedPacketSender Sender { get; }
			}

		private sealed class PacketCapture<T>: IPacketSender<T>
			{
				public List<T> Recipients { get; } = [];

				public void Send(T recipient, ReadOnlySpan<byte> packet)
					=> Recipients.Add(recipient);
			}
	}