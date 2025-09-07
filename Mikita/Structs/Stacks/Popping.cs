using System;
using System.Collections.Generic;

namespace Mikita.Structs.Stacks;

public static class Popping
	{
		public static IEnumerable<T> PopWhile<T>
			(
				this Stack<T> stack,
				Func<T, bool> predicate
			)
			{
				while (stack.Count > 0 && predicate(stack.Peek()))
					yield return stack.Pop();
			}

		public static IEnumerable<T> PopMany<T>
			(
				this Stack<T> stack,
				int count
			)
			{
				for (var i = 0; i < count; i++)
					yield return stack.Pop();
			}

		public static IEnumerable<T> PopUntil<T>
			(
				this Stack<T> stack,
				Func<T, bool> predicate
			)
			{
				while (stack.Count > 0 && !predicate(stack.Peek()))
					yield return stack.Pop();
			}
	}