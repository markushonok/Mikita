using System.Numerics;

namespace Mikita.UI.Rects.Roundness;

public readonly record struct RectRoundnessRecord<T>
	(
		T TopLeft,
		T TopRight,
		T BottomRight,
		T BottomLeft
	) 
	: RectRoundness<T>
	where T : INumber<T>;