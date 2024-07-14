namespace Mikita.Subroutine.Props;

public sealed class PropRecord<T>(T value) : Prop<T>
	{
		public T Value { get; set; } = value;
	}