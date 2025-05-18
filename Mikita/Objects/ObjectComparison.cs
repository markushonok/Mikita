namespace Mikita.Objects;

public static class ObjectComparison
	{
		public static bool NotEquals(this object a, object? b)
			=> !a.Equals(b);
	}