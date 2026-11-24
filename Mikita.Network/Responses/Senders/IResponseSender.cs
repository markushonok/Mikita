using Mikita.Network.Messages;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests;
using Mikita.Structs.Buffers.Writing;

namespace Mikita.Network.Responses.Senders;

public interface IResponseSender
	{
		void Send
			(
				PeerKey recipient,
				MessageTypeId typeId,
				RequestId requestId
			);

		void Send<T>
			(
				PeerKey recipient,
				MessageTypeId typeId,
				RequestId requestId,
				T message,
				WriterPutWay<RentedByteWriter, T> serialize
			);
	}