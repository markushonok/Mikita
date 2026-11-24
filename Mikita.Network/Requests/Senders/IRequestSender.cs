using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests.Contracts;
using Mikita.Structs.Buffers.Writing;
using Mikita.Threading;
using System.Threading;

namespace Mikita.Network.Requests.Senders;

public interface IRequestSender
	{
		IMatchTask<THook> Send<TMessage, THook>
			(
				PeerKey recipient,
				TMessage message,
				WriterPutWay<RentedByteWriter, TMessage> serialize,
				IRequestContract<THook> contract,
				CancellationToken cancel = default
			);
	}