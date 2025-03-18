namespace Mikita.Observation.Change;

public static partial class Interserved
	{
		public static ObservedSource<T> Observed<T>
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