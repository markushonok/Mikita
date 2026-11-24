using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Pipeline;
using System.Buffers.Binary;

namespace Mikita.Network.Test;

public static class MessageRoleRoutingTest
	{
		[Theory]
		[InlineData(MessageRole.Request)]
		[InlineData(MessageRole.Response)]
		[InlineData(MessageRole.Notice)]
		public static void RegisteredRoleSelectsDestination(MessageRole role)
			{
				var registry = MessageRegistry.New;
				var label = registry.Register
					(
						new MessageLabel
							(
								TypeName: "message",
								role
							)
					);
				var typeId = label.TypeId;
				var expected = new PacketCapture();
				var unexpected = new PacketCapture();
				var router = new MessageRoleRouter
					(
						registry,
						requests: role == MessageRole.Request
							? expected
							: unexpected,
						responses: role == MessageRole.Response
							? expected
							: unexpected,
						notices: role == MessageRole.Notice
							? expected
							: unexpected,
						rejected: unexpected
					);
				var peer = new PeerKey(13);
				var packet = PacketWith(typeId.Value);

				router.Handle(peer, packet);

				expected.Packets.ShouldHaveSingleItem();
				expected.Packets[0].Peer.ShouldBe(peer);
				expected.Packets[0].Bytes.ShouldBe(packet);
				unexpected.Packets.ShouldBeEmpty();
			}

		[Fact]
		public static void UnknownTypeIdUsesRejectedDestination()
			{
				var rejected = new PacketCapture();
				var router = new MessageRoleRouter
					(
						MessageRegistry.New,
						requests: Middleware.Idle,
						responses: Middleware.Idle,
						notices: Middleware.Idle,
						rejected
					);
				var peer = new PeerKey(17);
				var packet = PacketWith(typeId: 987);

				router.Handle(peer, packet);

				rejected.Packets.ShouldHaveSingleItem();
				rejected.Packets[0].Peer.ShouldBe(peer);
				rejected.Packets[0].Bytes.ShouldBe(packet);
			}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public static void TruncatedTypeIdUsesRejectedDestination(int length)
			{
				var messages = MessageRegistry.New;
				messages.Register
					(
						new MessageLabel
							(
								TypeName: "request",
								MessageRole.Request
							)
					);
				var rejected = new PacketCapture();
				var router = new MessageRoleRouter
					(
						messages,
						requests: Middleware.Idle,
						responses: Middleware.Idle,
						notices: Middleware.Idle,
						rejected
					);
				var peer = new PeerKey(19);
				var packet = new byte[length];

				router.Handle(peer, packet);

				rejected.Packets.ShouldHaveSingleItem();
				rejected.Packets[0].Peer.ShouldBe(peer);
				rejected.Packets[0].Bytes.ShouldBe(packet);
			}

		private static byte[] PacketWith(short typeId)
			{
				var packet = new byte[] { 0, 0, 71, 29, 0, 0, 19 };
				BinaryPrimitives.WriteInt16LittleEndian(packet, typeId);
				return packet;
			}

		private sealed class PacketCapture: IMiddleware
			{
				public void Handle(PeerKey peer, ReadOnlySpan<byte> bytes)
					=> Packets.Add((peer, bytes.ToArray()));

				public List<(PeerKey Peer, byte[] Bytes)> Packets { get; } = [];
			}
	}