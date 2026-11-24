using Mikita.Threading;
using System;

namespace Mikita.Network.Messages.Receipt;

public interface IMessageSource<T>
	: IMessageEvent<T>
	{
		void Raise
			(
				Action<Match<T>> pattern
			);
	}