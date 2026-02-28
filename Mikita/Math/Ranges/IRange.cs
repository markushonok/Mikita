namespace Mikita.Math.Ranges;

public interface IRange<out T>
	{
		T Min { get; }

		T Max { get; }
	}