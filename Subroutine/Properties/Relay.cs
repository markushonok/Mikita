using Mikita.Subroutine.Actions;

namespace Mikita.Subroutine.Properties;

public readonly struct Relay<T>
	(
		Return<T> @return,
		Assign<T> assign
	)
	: Property<T>
	{
		public T Value
			{
				get => @return();
				set => assign(value);
			}
	}

public static class Relay
	{
		public static Relay<T> That<T>
			(
				Return<T> returns,
				Assign<T> assigns
			)
			=> new(returns, assigns);
	} 