using System;

namespace Mikita.Observation.Unconferences;

public static class ManagedEvent
	{
		public static UnconferenceRecord<Action> Empty
			=> ManagedEvent.With(action: delegate {});

		public static UnconferenceRecord<Action> With(Action action)
			=> new
				(
					action,
					happened: delegate {}
				);

		public static UnconferenceRecord<T> With<T>(T action)
			=> new
				(
					action,
					happened: delegate {}
				);
	}