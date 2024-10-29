namespace Mikita.Many.Tends;

public interface Tend
	{
		static TendRecord<TCurrent, TTarget> Of<TCurrent, TTarget>
			(
				TCurrent current, 
				TTarget target
			)
			=> new(current, target);
	}