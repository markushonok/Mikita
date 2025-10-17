using Mikita.Observation.Events;
using Mikita.Structs.Scalars;

namespace Mikita.Observation.Change;

partial class Managed
	{
		public static ObservedScalar<T> Observed<T>
			(
				this T value
			)
			{
				var changed = Event.New;

				var scalar = Scalar
					.With(value)
					.WithCallback(changed.Raise);

				return new ObservedScalar<T>(scalar, changed);
			}
	}