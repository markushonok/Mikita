using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;

namespace Mikita.Network.Requests.Contracts;

public interface IRequestContract<in T>
	: IMessageMatchMap<T>
	{
		MessageTypeId TypeId { get; }
	}