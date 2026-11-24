using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests.Contracts;
using Mikita.Structs.Buffers.Writing;
using Mikita.Threading;
using System.Threading;

namespace Mikita.Network.Requests.Senders;

/// <summary>
/// Serializes access to another <see cref="IRequestSender"/>. The request
/// registry, response waits, and the packet writer are single-use and must
/// not be used concurrently.
/// </summary>
/// <param name="source">Sender receiving serialized calls.</param>
/// <param name="access">Serializes public API calls.</param>
public sealed class SerialRequestSender
	(
		IRequestSender source,
		Lock access
	)
	: IRequestSender
	{
		public IMatchTask<THook> Send<TMessage, THook>
			(
				PeerKey recipient,
				TMessage message,
				WriterPutWay<RentedByteWriter, TMessage> serialize,
				IRequestContract<THook> contract,
				CancellationToken cancel = default
			)
			{
				lock (access) return source.Send
					(
						recipient,
						message,
						serialize,
						contract,
						cancel
					);
			}
	}