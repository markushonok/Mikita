using System.Collections.Generic;

namespace Mikita.Structs.Dictionaries;

public static class DictionaryAccess
	{
		public static TValue Pop<TKey, TValue>
			(
				this IDictionary<TKey, TValue> dictionary,
				TKey key
			)
			{
				if (!dictionary.Remove(key, out var value))
					{
						throw new KeyNotFoundException(
							$"Key {key} was not found in the dictionary.");
					}
				return value;
			}
	}