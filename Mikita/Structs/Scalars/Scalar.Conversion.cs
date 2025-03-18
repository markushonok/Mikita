using Mikita.Many.Scalars;

namespace Mikita.Structs.Scalars;

public static partial class Scalar
	{
		public static RelayScalar<T> AsScalar<T>
			(
				this Reference<T> reference
			)
			=> Scalar.That
				(
					returns: () => reference.Referent,
					assigns: value => reference.Referent = value
				);
	}