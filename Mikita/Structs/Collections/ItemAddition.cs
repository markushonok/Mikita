using System.Collections.Generic;

namespace Mikita.Structs.Collections;

public static class ItemAddition
	{
		public static void Add<T>
			(
				this ICollection<T> collection,
				IEnumerable<T> items
			)
			{
				foreach (var item in items)
					collection.Add(item);
			}
	}