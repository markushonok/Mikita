using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests;

namespace Mikita.Network.Pipeline;

public interface IMiddlewareRR<in T>
	{
		void Handle
			(
				PeerKey peer,
				RequestId requestId,
				T message
			);
	}