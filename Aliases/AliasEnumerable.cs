namespace Mikita.Aliases;

public static class AliasEnumerable
	{
		public static IEnumerable<T> FullTyped<T>
			(
				this IEnumerable<Alias<T>> enumerable
			)
			=> enumerable.Select(alias => alias.FullTyped);
	}