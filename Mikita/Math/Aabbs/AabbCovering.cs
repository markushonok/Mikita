using Mikita.Math.Vectors;
using Mikita.Math.Vectors.Spatial;
using System.Numerics;

namespace Mikita.Math.Aabbs;

public static class AabbCovering
	{
		extension<T>(IAabb3D<T> outer) where T : INumber<T>, IRootFunctions<T>
			{
				public bool NotCovers
					(
						IAabb3D<T> inner
					)
					=> !outer.Covers(inner);

				public bool Covers
					(
						IAabb3D<T> inner
					)
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

				public bool NotCovers
					(
						IVector3D<T> point
					)
					=> !outer.Covers(point);

				public bool Covers
					(
						IVector3D<T> point
					)
					{
						var isGreaterThanA
							=  point.X >= outer.Min.X
							&& point.Y >= outer.Min.Y
							&& point.Z >= outer.Min.Z;

						var isLessThanB
							=  point.X <= outer.Max.X
							&& point.Y <= outer.Max.Y
							&& point.Z <= outer.Max.Z;

						return isGreaterThanA && isLessThanB;
					}

				public T MaxLength
					{
						get
							{
								var size = outer.Size;
								return T.Max(size.X, T.Max(size.Y, size.Z));
							}
					}

				public IVector3D<T> Size
					=> outer.Max - outer.Min;
			}

	}