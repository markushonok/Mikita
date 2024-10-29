namespace Mikita.Observing;

public static class ArtificialSource
	{
		public static ArtificialSource<T> Observed<T>
			(
				this T referent
			)
			=> new
				(
					referent,
					changing: delegate {},
					changed: delegate {}
				);
	}