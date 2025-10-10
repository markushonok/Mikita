using Mikita.Evaluation.Liveness;
using Mikita.Many.Scalars;
using System;

namespace Mikita.Structs.Scalars;

public static partial class Scalar
	{
		public static TTarget LiveValue
			<
				TSource,
				TTarget
			>
			(
				this Scalar<TSource> scalar,
				Func<TSource, TTarget> pattern
			)
			where TTarget: class
			=> Live.ResultOf(() => pattern(scalar.Value));

		public static T LiveValue<T>
			(
				this Scalar<T> scalar
			)
			where T: class
			=> scalar.Live(x => x.Value);

		public static Func<T> AsFunction<T>
			(
				this Scalar<T> scalar
			)
			=> () => scalar.Value;

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