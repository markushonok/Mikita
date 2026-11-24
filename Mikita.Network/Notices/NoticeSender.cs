using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Packets.Senders;
using Mikita.Network.Peers.Keys;
using Mikita.Structs.Buffers.Writing;

namespace Mikita.Network.Notices;

public sealed partial class NoticeSender
	{
		public static INoticeSender With
			(
				IMessageRegistry messages,
				IPacketSender<PeerKey> sender
			)
			=> new NoticeSender(messages, sender, ByteWriterPool.New);
	}

public sealed partial class NoticeSender
	(
		IMessageRegistry messages,
		IPacketSender<PeerKey> sender,
		IByteWriterPool writers
	)
	: INoticeSender
	{
		public void Send<T>
			(
				PeerKey recipient,
				MessageTypeId typeId,
				T message,
				WriterPutWay<RentedByteWriter, T> serialize
			)
			{
				messages.Require(typeId, MessageRole.Notice);
				using var writer = writers.Rent();
				writer.Put(typeId.Value);
				writer.Put(serialize, message);
				sender.Send(recipient, writer.Written);
			}
	}