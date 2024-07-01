namespace Mikita.Subroutine.Props;

public interface ReadOnlyProp<out T>
	{
		T Value { get; }
	}