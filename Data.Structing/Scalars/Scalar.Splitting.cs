using Mikita.Subroutine.Actions;
using Mikita.Subroutine.Relays;

namespace Mikita.Many.Scalars;

public partial interface Scalar<T>
	{
		public Assign<T> WriteOnly
			=> value => Value = value;
	}

public static partial class Scalar
	{
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