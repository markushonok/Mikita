using Mikita.Structs.Scalars;

namespace Mikita.Observation.Change;

partial class Managed
	{
		public static RelayScalar<T> AsScalar<T>
			(
				this IManaged<T> managed
			)
			=> Scalar.That
				(
					returns: () => managed.Current,
					assigns: value => managed.Current = value
				);
	}