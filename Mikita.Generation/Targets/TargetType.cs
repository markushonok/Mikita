namespace Mikita.Generation.Targets;

public sealed record TargetType
	(
		string Name,
		int Arity,
		TargetProducts Products
	)
	: ITargetType;