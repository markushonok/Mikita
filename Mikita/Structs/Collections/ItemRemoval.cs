using System.Collections.Generic;

namespace Mikita.Structs.Collections;

public static class ItemRemoval
	{
		public static void Remove<T>
			(
				this ICollection<T> collection,
				IEnumerable<T> items
			)
			{
				foreach (var item in items)
					collection.Remove(item);
			}
	}