using Mikita.Subroutine.Actions;
using Mikita.Subroutine.Relays;
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
			=> Relay.To(scalar.ToFunc());

		public static Func<T> ToFunc<T>
			(
				this Scalar<T> scalar
			)
			=> () => scalar.Value;
	}