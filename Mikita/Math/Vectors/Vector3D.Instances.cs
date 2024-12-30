namespace Mikita.Math.Vectors;

public partial interface Vector3D<out T>
	{
		public static VectorRecord3D<T> Zero
			=> Vector.PointingTo(T.Zero, T.Zero, T.Zero);
	}