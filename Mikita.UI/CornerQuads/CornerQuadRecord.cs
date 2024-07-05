using System.Numerics;

namespace Mikita.UI.CornerQuads;

public readonly record struct CornerQuadRecord<T>
	(
		T A,
		T B,
		T C,
		T D
	) 
	: CornerQuad<T>
	where T : INumber<T>;