using System;

namespace Mikita.Structs.Maps;

public sealed class ReadOnlyMap<TKey, T>
	(
		Func<TKey, T> receipt
	)
	: IReadOnlyMap<TKey, T>
	{
		public T this[TKey key]
			=> receipt(key);
	}