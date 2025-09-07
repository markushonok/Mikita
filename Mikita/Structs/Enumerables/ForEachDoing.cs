using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mikita.Structs.Enumerables;

public static class ForEachDoing
	{
		public static void ForEach<T>
			(
				this IEnumerable<T> enumerable,
				Action<T> action
			)
			{
				foreach (var element in enumerable)
					action(element);
			}

		public static async Task ForEachAsync<T>
			(
				this IEnumerable<T> enumerable,
				Func<T, Task> action
			)
			{
				foreach (var element in enumerable)
					await action(element);
			}
	}