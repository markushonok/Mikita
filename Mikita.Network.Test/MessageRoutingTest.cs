using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Pipeline;
using System.Buffers.Binary;

namespace Mikita.Network.Test;

public static class MessageRoutingTest
	{
		[Fact]
		public static void RegisteredIdSelectsPayloadDestination()
			{
				var registry = MessageRegistry.New;
				var firstLabel = registry.Register
					(
						new MessageLabel
							(
								TypeName: "message-1",
								MessageRole.Notice
							)
					);
				var first = firstLabel.TypeId;
				var secondLabel = registry.Register
					(
						new MessageLabel
							(
								TypeName: "message-2",
								MessageRole.Notice
							)
					);
				var second = secondLabel.TypeId;
				var selected = new PacketCapture();
				var unexpected = new PacketCapture();
				var router = MessageTypeRouter.NewWith(typeMiss: unexpected);
				router.Add(unexpected.With(first));
				router.Add(selected.With(second));
				var peer = new PeerKey(17);
				var packet = PacketWith(second);

				router.Handle(peer, packet);

				selected.Packets.ShouldHaveSingleItem();
				selected.Packets[0].Peer.ShouldBe(peer);
				selected.Packets[0].Bytes.ShouldBe(packet[sizeof(short)..]);
				unexpected.Packets.ShouldBeEmpty();
			}

		[Fact]
		public static void RemovedIdUsesMissDestination()
			{
				var registry = MessageRegistry.New;
				var label = registry.Register
					(
						new MessageLabel
							(
								TypeName: "message-3",
								MessageRole.Notice
							)
					);
				var message = label.TypeId;
				var selected = new PacketCapture();
				var missed = new PacketCapture();
				var router = MessageTypeRouter.NewWith(typeMiss: missed);
				router.Add(selected.With(message));
				router.Remove(message);
				var packet = PacketWith(message);
				var peer = new PeerKey(19);

				router.Handle(peer, packet);

				missed.Packets.ShouldHaveSingleItem();
				missed.Packets[0].Peer.ShouldBe(peer);
				missed.Packets[0].Bytes.ShouldBe(packet);
				selected.Packets.ShouldBeEmpty();
			}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public static void TruncatedIdUsesMissDestination(int length)
			{
				var missed = new PacketCapture();
				var router = MessageTypeRouter.NewWith(typeMiss: missed);
				var packet = new byte[length];
				var peer = new PeerKey(23);

				router.Handle(peer, packet);

				missed.Packets.ShouldHaveSingleItem();
				missed.Packets[0].Peer.ShouldBe(peer);
				missed.Packets[0].Bytes.ShouldBe(packet);
			}

		private static byte[] PacketWith(MessageTypeId typeId)
			{
				var packet = new byte[] { 0, 0, 42, 71, 19 };
				BinaryPrimitives.WriteInt16LittleEndian(packet, typeId.Value);
				return packet;
			}

		private sealed class PacketCapture: IMiddleware
			{
				public void Handle(PeerKey peer, ReadOnlySpan<byte> bytes)
					=> Packets.Add((peer, bytes.ToArray()));

				public List<(PeerKey Peer, byte[] Bytes)> Packets { get; } = [];
			}
	}