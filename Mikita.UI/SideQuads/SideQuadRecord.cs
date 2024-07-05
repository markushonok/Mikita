using System.Numerics;

namespace Mikita.UI.SideQuads;

public readonly record struct SideQuadRecord<T>
	(
		T Left,
		T Top,
		T Right,
		T Bottom
	) 
	: SideQuad<T>
	where T : INumber<T>;