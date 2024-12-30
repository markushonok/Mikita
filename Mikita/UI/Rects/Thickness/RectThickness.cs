using System.Numerics;

namespace Mikita.UI.Rects.Thickness;

public partial interface RectThickness<out T> 
	where T : INumber<T>
	{
		T Left { get; }

		T Top { get; }

		T Right { get; }

		T Bottom { get; }
	}