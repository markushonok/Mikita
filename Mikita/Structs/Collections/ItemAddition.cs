using System.Collections.Generic;

namespace Mikita.Structs.Collections;

public static class ItemPlacement
	{
		public static void Replace<T>
			(
				this ICollection<T> collection,
				T from,
				T to
			)
			{
				collection.Remove(from);
				collection.Add(to);
			}

		public static void Add<T>
			(
				this ICollection<T> collection,
				IEnumerable<T> items
			)
			{
				foreach (var item in items)
					collection.Add(item);
			}

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