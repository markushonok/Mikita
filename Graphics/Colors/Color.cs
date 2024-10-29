namespace Mikita.Graphics.Colors;

public interface Color<out T>
	{
		T Red { get; }
		
		T Green { get; }
		
		T Blue { get; }
		
		T Alpha { get; }
	}