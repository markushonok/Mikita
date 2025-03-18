using Mikita.Structs.Scalars;

namespace Mikita.Observation.Change;

public static partial class Interserved
	{
		internal static RelayScalar<T> AsScalar<T>
			(
				this Interserved<T> interserved
			)
			=> Scalar.That
				(
					returns: () => interserved.Current,
					assigns: value => interserved.Current = value
				);
	}