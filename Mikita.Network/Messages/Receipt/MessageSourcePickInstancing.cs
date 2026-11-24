using Mikita.Threading;
using System.Collections.Generic;

namespace Mikita.Network.Messages.Receipt;

public static class MessageSourcePickInstancing
	{
		extension<T>(MessageSourceMap<T>)
			{
				public static IMessageSourceMap<T> New
					=> new MessageSourceMap<T>
						(
							new Dictionary<object, IList<Match<T>>>()
						);
			}
	}