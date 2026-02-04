using Mikita.Math.Numbers;
using Mikita.Math.Points;
using System.Numerics;

namespace Mikita.Math.Vectors.Planar;

public static class VectorTrait2D
	{
		extension<T>(IVector2D<T> vector)
			where T: INumber<T>, IRootFunctions<T>
			{
				public Vector2D<T> Sign
					=> Vector.PointingTo
						(
							vector.X.TSign,
							vector.Y.TSign
						);

				public Point2D<T> Point
				=> new(vector.X, vector.Y);

				public Vector2D<T> Normal
					=> vector / vector.Length;

				public T Length
					=> T.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			}
	}