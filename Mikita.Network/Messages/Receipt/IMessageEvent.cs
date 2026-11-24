using Mikita.Threading;

namespace Mikita.Network.Messages.Receipt;

public interface IMessageEvent<out T>
	{
		void Add(Match<T> match);

		void Remove(Match<T> match);
	}