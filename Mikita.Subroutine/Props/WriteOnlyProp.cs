namespace Mikita.Subroutine.Props;

public interface WriteOnlyProp<in T>
	{
		T Value { set; }
	}