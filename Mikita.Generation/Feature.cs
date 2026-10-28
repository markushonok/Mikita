namespace Mikita.Generation;

public static class Feature
	{
		public static bool Known(string name)
			=> name == Live;

		public const string Live = "live";
	}