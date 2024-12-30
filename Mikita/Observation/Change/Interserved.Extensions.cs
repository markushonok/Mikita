using Mikita.Structs.Scalars;

namespace Mikita.Observation.Change;

public partial interface Interserved<T>
	{
		RelayScalar<T> AsScalar
			=> Scalar.That
				(
					returns: () => Current,
					assigns: value => Current = value
				);
	}