using Mikita.Subroutine.Actions;

namespace Mikita.Many.Scalars;

public sealed class RelayScalar<T>
	(
		Func<T> @return,
		Assign<T> assign
	)
	: Scalar<T>
	{
		public T Value
			{
				get => @return();
				set => assign(value);
			}
	}

public static class RelayScalar
	{
		public static RelayScalar<T> That<T>
			(
				Func<T> returns,
				Assign<T> assigns
			)
			=> new(returns, assigns);
	} 