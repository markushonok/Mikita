using System.Collections.Generic;

namespace Mikita.Structs.Stacks;

public static class StackConversion
	{
		public static Stack<T> ToStack<T>
			(
				this IEnumerable<T> enumerable
			)
			=> new(enumerable);
	}