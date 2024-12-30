using System.Numerics;

namespace Mikita.Math.Vectors;

public static partial class Vector
	{
		public static Vector2D<T> InMiddleBetween<T>
			(
				params Vector2D<T>[] vectors
			)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.InMiddleBetween(vectors.ToList());

		public static Vector2D<T> InMiddleBetween<T>
			(
				IEnumerable<Vector2D<T>> vectors
			)
			where T : INumber<T>, IRootFunctions<T>
			=> Vector.InMiddleBetween(vectors.ToList());

		public static Vector2D<T> InMiddleBetween<T>
			(
				ICollection<Vector2D<T>> vectors
			)
			where T : INumber<T>, IRootFunctions<T>
			=> vectors.Aggregate((left, right) => left + right) 
					/ T.CreateChecked(vectors.Count);

		public static Vector2D<T> InMiddleBetween<T>
			(
				Vector2D<T> a,
				Vector2D<T> b
			)
			where T : INumber<T>, IRootFunctions<T>
			=> (a + b) / T.CreateChecked(2);
	}