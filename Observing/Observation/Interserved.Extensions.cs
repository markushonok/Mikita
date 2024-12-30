using Mikita.Many.Scalars;

namespace Mikita.Observing.Observation;

public partial interface Interserved<T>
	{
		RelayScalar<T> AsScalar
			=> Scalar.That
				(
					returns: () => Current,
					assigns: value => Current = value
				);
	}