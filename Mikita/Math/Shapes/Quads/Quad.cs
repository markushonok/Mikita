using Mikita.Math.Vectors;
using System.Numerics;

namespace Mikita.Math.Shapes.Quads;

public partial interface Quad<out T>
	where T : INumber<T>, IRootFunctions<T>
	{
		Vector2D<T> TopLeft { get; }
		
		Vector2D<T> TopRight { get; }
		
		Vector2D<T> BottomLeft { get; }
		
		Vector2D<T> BottomRight { get; }
	}