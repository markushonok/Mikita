using System.Collections.Generic;

namespace Mikita.Structs.Collections;

public static class ItemPlacement
	{
		extension<T>(ICollection<T> collection)
			{
				public void Replace
					(
						T from,
						T to
					)
					{
						collection.Remove(from);
						collection.Add(to);
					}

				public void Add
					(
						IEnumerable<T> items
					)
					{
						foreach (var item in items)
							collection.Add(item);
					}

				public void Remove
					(
						IEnumerable<T> items
					)
					{
						foreach (var item in items)
							collection.Remove(item);
					}
			}

	}