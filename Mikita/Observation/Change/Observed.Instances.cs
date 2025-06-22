using Mikita.Observation.Events;
using Mikita.Structs.Scalars;
using System;

namespace Mikita.Observation.Change;

public static class Observed
	{
		public static ObservedScalar<T> With<T>
			(
				this Scalar<T> current,
				Event<Action> changed
			)
			=> new(current, changed);
	}