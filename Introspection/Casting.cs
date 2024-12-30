namespace Introspection;

public static class Casting
	{
		public static T Base<T>(this T @object)
			=> @object;
	}