using System;
using System.Collections.Generic;

namespace Mikita.Structs.Enumerables;

public static class ForEachDoing
	{
		public static void ForEachDo<T>
			(
				this IEnumerable<T> enumerable,
				Action<T> action
			)
			{
				foreach (var element in enumerable)
					action(element);
			}
	}