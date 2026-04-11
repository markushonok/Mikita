using System.Collections.Generic;

namespace Mikita.Structs.Enumerables;

public static class Counting
	{
		extension<T>(IEnumerable<T> source)
			{
				public int IndexOf
					(
						T item,
						int @default = -1
					)
					{
						var index = 0;

						foreach (var element in source)
							{
								if (Equals(element, item))
									return index;

								index++;
							}

						return @default;
					}
			}
	}