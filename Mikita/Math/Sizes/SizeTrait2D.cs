using Mikita.Math.Vectors.Planar;
using System.Numerics;

namespace Mikita.Math.Sizes;

public static class SizeTrait2D
	{
		extension<T>(ISize2D<T> size)
			where T: INumber<T>, IRootFunctions<T>
			{
				public T Diagonal
					=> size.AsVector.Length;
			}
	}