namespace Mikita.Strings;

public static class StringReplacement
	{
		public static string Replace
			(
				this string origin,
				string from,
				object to
			)
			=> origin.Replace(from, to.ToString());
	}