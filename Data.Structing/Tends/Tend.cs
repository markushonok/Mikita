namespace Mikita.Many.Tends;

public interface Tend<out TCurrent, out TTarget>
	{
		TCurrent Current { get; }
		
		TTarget Target { get; }
	}