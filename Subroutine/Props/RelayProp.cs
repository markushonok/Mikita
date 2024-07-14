namespace Mikita.Subroutine.Props;

public readonly struct RelayProp<T>
	(
		Func<T> accessor,
		Action<T> mutator
	)
	: Prop<T>
	{

		public T Value
			{
				get => accessor();
				set => mutator(value);
			}
	}