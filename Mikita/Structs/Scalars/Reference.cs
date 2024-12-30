using Mikita.Structs.Scalars;

namespace Mikita.Many.Scalars;

public sealed class Reference<T>
	{
		public required T Referent { get; set; }

		public RelayScalar<T> AsScalar

			=> Scalar.That
				(
					returns: () => Referent,
					assigns: value => Referent = value
				);
	}

public static class Reference
	{
		public static Reference<T> To<T>
			(
				T referent
			)
			=> new() {Referent = referent};
	} 