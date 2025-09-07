using Mikita.Evaluation.Liveness;
using Mikita.Subroutine.Actions;
using System;

namespace Mikita.Structs.Scalars;

public static partial class Scalar
	{
		public static Assign<T> WriteOnly<T>
			(
				this Scalar<T> scalar
			)
			=> value => scalar.Value = value;

		public static T ReadOnly<T>
			(
				this Scalar<T> scalar
			)
			where T: class
			=> scalar.Live(x => x.Value);

		public static Func<T> ToFunc<T>
			(
				this Scalar<T> scalar
			)
			=> () => scalar.Value;
	}