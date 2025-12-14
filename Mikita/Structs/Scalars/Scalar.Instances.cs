using Mikita.Many.Scalars;
using Mikita.Routines.Assignment;
using System;

namespace Mikita.Structs.Scalars;

public static partial class Scalar
	{
		public static RelayScalar<T> That<T>
			(
				Func<T> returns,
				Assign<T> assigns
			)
			=> new(returns, assigns);

		public static RelayScalar<T?> Null<T>()
			where T: class
			=> Scalar.With<T?>(null);

		public static RelayScalar<T> With<T>
			(
				T value
			)
			=> Reference.To(value).AsScalar();

		public static BackCallingScalar<T> WithCallback<T>
			(
				this Scalar<T> origin,
				Action<Scalar<T>> callback
			)
			=> new(origin, () => callback(origin));

		public static BackCallingScalar<T> WithCallback<T>
			(
				this Scalar<T> origin,
				Action<T> callback
			)
			=> new(origin, () => callback(origin.Value));

		public static BackCallingScalar<T> WithCallback<T>
			(
				this Scalar<T> origin,
				Action callback
			)
			=> new(origin, callback);
	} 