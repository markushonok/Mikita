namespace Mikita.Observation.Change;

public static class ManagedChangeOf
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