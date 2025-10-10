using Mikita.Nulls;

namespace Mikita.Structs.Scalars;

public static class NullScalarAssert
	{
		public static Scalar<T> NotNull<T>
			(
				this Scalar<T?> scalar
			)
			=> Scalar.That
				(
					returns: () => scalar.Value.NotNull(),
					assigns: value => scalar.SetTo(value.NotNull())
				);
	}