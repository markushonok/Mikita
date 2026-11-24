using Mikita.Network.Messages;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests;
using Mikita.Structs.Buffers.Writing;
using Mikita.Threading;
using System.Threading;

namespace Mikita.Network.Responses.Senders;

/// <summary>
/// Serializes access to another <see cref="IResponseSender"/>. The packet
/// writer is single-use and must not be used concurrently.
/// </summary>
/// <param name="source">Sender receiving serialized calls.</param>
/// <param name="access">Serializes public API calls.</param>
public sealed class SerialResponseSender
	(
		IResponseSender source,
		Lock access
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
				lock (access) source.Send(recipient, typeId, requestId);
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
				lock (access) source.Send
					(
						recipient,
						typeId,
						requestId,
						message,
						serialize
					);
			}
	}