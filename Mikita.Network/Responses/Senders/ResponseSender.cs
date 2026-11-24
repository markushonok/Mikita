using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Packets.Senders;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests;
using Mikita.Structs.Buffers.Writing;

namespace Mikita.Network.Responses.Senders;

public sealed partial class ResponseSender
	{
		public static IResponseSender With
			(
				IMessageRegistry messages,
				IPacketSender<PeerKey> sender
			)
			=> new ResponseSender
				(
					messages,
					sender,
					ByteWriterPool.New
				)
				.WithSerialAccess;
	}

public sealed partial class ResponseSender
	(
		IMessageRegistry messages,
		IPacketSender<PeerKey> sender,
		IByteWriterPool writers
	)
	: IResponseSender
	{
		public void Send
			(
				PeerKey recipient,
				MessageTypeId typeId,
				RequestId requestId
			)
			{
				using var writer = writers.Rent();
				messages.Require(typeId, MessageRole.Response);
				writer.Put(typeId.Value);
				writer.Put(requestId);
				sender.Send(recipient, writer.Written);
			}

		public void Send<T>
			(
				PeerKey recipient,
				MessageTypeId typeId,
				RequestId requestId,
				T message,
				WriterPutWay<RentedByteWriter, T> serialize
			)
			{
				using var writer = writers.Rent();
				messages.Require(typeId, MessageRole.Response);
				writer.Put(typeId.Value);
				writer.Put(requestId);
				writer.Put(serialize, message);
				sender.Send(recipient, writer.Written);
			}
	}