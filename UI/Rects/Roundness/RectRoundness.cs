using System.Numerics;

namespace Mikita.UI.Rects.Roundness;

public interface RectRoundness<out T> 
	where T : INumber<T>
	{
		T TopLeft { get; }

		T TopRight { get; }

		T BottomRight { get; }

		T BottomLeft { get; }
	}