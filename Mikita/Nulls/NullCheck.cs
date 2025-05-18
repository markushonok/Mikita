using Mikita.Structs.Tuples;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mikita.Nulls;

public static class NullCheck
	{
		public static bool HasNotNull(this ITuple tuple)
			=> !tuple.HasNull();

		public static bool HasNull(this ITuple tuple)
			=> tuple.ToEnumerable().Any(x => x == null);
	}