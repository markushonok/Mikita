using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;

namespace Mikita.Network.Requests.Contracts;

public static class RequestContract
	{
		extension<T>(IMessageMatchMap<T> source)
			{
				public IRequestContract<T> ForRequest(MessageTypeId typeId)
					=> new RequestContract<T>
						(
							typeId,
							matches: source
						);
			}
	}

public sealed class RequestContract<T>
	(
		MessageTypeId typeId,
		IMessageMatchMap<T> matches
	)
	: IRequestContract<T>
	{
		public IMessageMatch<T>? this[MessageTypeId typeId]
			=> matches[typeId];

		public MessageTypeId TypeId
			=> typeId;
	}