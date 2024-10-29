using Mikita.Math.Points;
using System.Numerics;

namespace Mikita.Math.Shapes.Quads;

public partial interface Quad<out T>
	where T : INumber<T>
	{
		Point2D<T> TopLeft { get; }
		
		Point2D<T> TopRight { get; }
		
		Point2D<T> BottomLeft { get; }
		
		Point2D<T> BottomRight { get; }
	}