namespace Mikita.Math.Vectors;

public partial interface Vector2D<out T>
	{
		static VectorRecord2D<T> Zero
			=> Vector.PointingTo(T.Zero, T.Zero);
	}