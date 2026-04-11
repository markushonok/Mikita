using System.Collections.Generic;
using System.Linq;

namespace Mikita.Structs.Enumerables;

public static class Filtering
	{
		public static IEnumerable<T> WhereNotNull<T>
			(
				this IEnumerable<T?> source
			)
			where T: class
			=> source.Where(x => x != null)!;

		public static IEnumerable<T> WhereNotNull<T>
			(
				this IEnumerable<T?> source
			)
			where T: struct
			{
				return source
					.Where(x => x.HasValue)
					.Select(x => x!.Value);
			}
	}