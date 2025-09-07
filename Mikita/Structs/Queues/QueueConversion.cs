using System.Collections.Generic;

namespace Mikita.Structs.Queues;

public static class QueueConversion
	{
		public static Queue<T> ToQueue<T>
			(
				this IEnumerable<T> enumerable
			)
			=> new(enumerable);
	}