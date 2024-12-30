namespace Mikita.Structs.Tends;

public interface Tend<out TCurrent, out TTarget>
	{
		TCurrent Current { get; }
		
		TTarget Target { get; }
	}