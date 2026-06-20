using System;
using System.Collections.Generic;

namespace Mikita.Structs.Maps;

public static class Map
	{
		public static IReadOnlyMap<TKey, T> Of<TKey, T>
			(
				Func<TKey, T> receipt
			)
			=> new ReadOnlyMap<TKey, T>(receipt);

		extension<TKey, TValue>
			(
				IDictionary<TKey, TValue> dictionary
			)
			{
				public IReadOnlyMap<TKey, TValue> AsMap
					=> Of<TKey, TValue>(key => dictionary[key]);
			}
	}