using System.Collections.Generic;

namespace Mikita.Structs.Cursors;

public static class CursorInstancing
	{
		extension<TKey, TValue>
			(
				IDictionary<TKey, TValue> dictionary
			)
			{
				public Cursor<TValue> CursorTo
					(
						TKey key
					)
					=> new
						(
							current: () => dictionary[key],
							has: () => dictionary.ContainsKey(key),
							add: item => dictionary.Add(key, item),
							remove: () => dictionary.Remove(key)
						);
			}
	}