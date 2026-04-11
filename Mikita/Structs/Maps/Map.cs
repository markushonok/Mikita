using System;

namespace Mikita.Structs.Maps;

public static class Map
	{
		public static IReadOnlyMap<TKey, T> Of<TKey, T>
			(
				Func<TKey, T> receipt
			)
			=> new ReadOnlyMap<TKey, T>(receipt);
	}