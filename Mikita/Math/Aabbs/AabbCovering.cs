using Mikita.Math.Vectors;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public static class AabbCovering
	{
		public static bool NotCovers<T>
			(
				this IAabb3D<T> outer,
				IAabb3D<T> inner
			)
			where T : INumber<T>, IRootFunctions<T>
			=> !outer.Covers(inner);

		public static bool Covers<T>
			(
				this IAabb3D<T> outer,
				IAabb3D<T> inner
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				var isAInside
					=  inner.Min.X >= outer.Min.X
					&& inner.Min.Y >= outer.Min.Y
					&& inner.Min.Z >= outer.Min.Z;

				var isBInside
					=  inner.Max.X <= outer.Max.X
					&& inner.Max.Y <= outer.Max.Y
					&& inner.Max.Z <= outer.Max.Z;

				return isAInside && isBInside;
			}

		public static bool NotCovers<T>
			(
				this IAabb3D<T> aabb,
				IVector3D<T> point
			)
			where T : INumber<T>, IRootFunctions<T>
			=> !aabb.Covers(point);

		public static bool Covers<T>
			(
				this IAabb3D<T> aabb,
				IVector3D<T> point
			)
			where T : INumber<T>, IRootFunctions<T>
			{
				var isGreaterThanA
					=  point.X >= aabb.Min.X
					&& point.Y >= aabb.Min.Y
					&& point.Z >= aabb.Min.Z;

				var isLessThanB
					=  point.X <= aabb.Max.X
					&& point.Y <= aabb.Max.Y
					&& point.Z <= aabb.Max.Z;

				return isGreaterThanA && isLessThanB;
			}

		public static T MaxLength<T>
			(
				this IAabb3D<T> aabb
			)
			where T: INumber<T>, IRootFunctions<T>
			{
				var size = aabb.Size();
				return T.Max(size.X, T.Max(size.Y, size.Z));
			}

		public static IVector3D<T> Size<T>
			(
				this IAabb3D<T> aabb
			)
			where T: INumber<T>, IRootFunctions<T>
			=> aabb.Max - aabb.Min;

	}