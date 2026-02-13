using Mikita.Math.Indices;
using Mikita.Structs.Arrays;
using Mikita.Structs.Referring;
using System;
using System.Collections.Generic;

namespace Mikita.Structs.Cursors;

public static class CursorInstancing
	{
		extension<T>(ICursor<T> cursor)
			{
				public CallbackCursor<T> WithCallbackOn
					(
						Action add,
						Action remove
					)
					=> new
						(
							target: cursor,
							add: index => add(),
							remove
						);

				public CallbackCursor<T> WithCallbackOn
					(
						Action<T> add,
						Action remove
					)
					=> new(target: cursor, add, remove);
			}

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

		extension<T>
			(
				IRef<IArray2D<T?>> array
			)
			{
				public ArrayCursor2D<T> CursorTo
					(
						IIndex2D index
					)
					=> new(array, index);
			}
	}