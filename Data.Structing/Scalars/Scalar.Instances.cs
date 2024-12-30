using Mikita.Subroutine.Actions;

namespace Mikita.Many.Scalars;

public static partial class Scalar
	{
		public static RelayScalar<T> That<T>
			(
				Func<T> returns,
				Assign<T> assigns
			)
			=> new(returns, assigns);

		public static RelayScalar<T> With<T>
			(
				T value
			)
			=> Reference.To(value).AsScalar;

		public static BackCallingScalar<T> WithCallback<T>
			(
				this Scalar<T> origin,
				Action callback
			)
			=> new(origin, callback);
	} 