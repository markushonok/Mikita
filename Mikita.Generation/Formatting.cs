namespace Mikita.Generation;

public static class Formatting
	{
		public static string Tab
			(
				int count = 1
			)
			=> new('\t', count);
	}