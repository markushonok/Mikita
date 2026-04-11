using System.Collections.Generic;

namespace Mikita.Structs.Enumerables;

public static class EnumerableComparison
	{
		extension<T>(IEnumerable<T> source)
			{
				public IEnumerable<T> SequenceEquatable
					=> new EquatableEnumerable<T>(source);
			}
	}