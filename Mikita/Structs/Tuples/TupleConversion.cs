using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mikita.Structs.Tuples;

public static class TupleConversion
	{
		public static IEnumerable<object?> ToEnumerable(this ITuple tuple)
			{
				for (var i = 0; i < tuple.Length; i++)
					yield return tuple[i];
			}
	}