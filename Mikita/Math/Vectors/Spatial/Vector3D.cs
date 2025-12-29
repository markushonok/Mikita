namespace Mikita.Math.Vectors.Spatial;

public sealed partial class Vector3D<T>
	(
		T x,
		T y,
		T z
	) 
	: IVector3D<T>
	{
		public T X => x;

		public T Y => y;

		public T Z => z;
	}

public static class Vector3D;