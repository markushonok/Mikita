using System.Numerics;

namespace Mikita.UI.Rects.Thickness;

public readonly record struct RectThicknessRecord<T>
	(
		T Left,
		T Top,
		T Right,
		T Bottom
	) 
	: RectThickness<T>
	where T : INumber<T>;