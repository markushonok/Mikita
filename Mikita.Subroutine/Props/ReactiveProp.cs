namespace Mikita.Subroutine.Props;

public interface ReactiveProp<T> : Prop<T>
	{
		event Action Changed;
	}