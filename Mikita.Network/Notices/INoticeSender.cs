using Mikita.Network.Messages;
using Mikita.Network.Peers.Keys;
using Mikita.Structs.Buffers.Writing;

namespace Mikita.Network.Notices;

/// <summary>
/// Sends a notice to a peer. Serialize calls when sharing a sender.
/// </summary>
public interface INoticeSender
	{
		void Send<T>
			(
				PeerKey recipient,
				MessageTypeId typeId,
				T message,
				WriterPutWay<RentedByteWriter, T> serialize
			);
	}