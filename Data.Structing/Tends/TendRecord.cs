namespace Mikita.Many.Tends;

public sealed record TendRecord<TCurrent, TTarget>
	(
		TCurrent Current,
		TTarget Target
	) 
	: Tend<TCurrent, TTarget>;