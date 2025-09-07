using System.Linq;
using System.Runtime.CompilerServices;

namespace Mikita.Structs.Tuples;

public static class TupleContaining
	{
		public static bool NotContains
			(
				this ITuple tuple,
				object? value
			)
			=> !tuple.Contains(value);

		public static bool Contains
			(
				this ITuple tuple,
				object? value
			)
			=> tuple.ToEnumerable().Contains(value);
	}