using System.Numerics;

namespace Mikita.Math.Functions;

public static class Ease
	{
		public static T Linear<T>(T x) => x;

		public static T OutCubic<T>(T x)
			where T: INumber<T>, IFloatingPointIeee754<T>
			{
				return T.One - T.Pow(T.One - x, T.CreateChecked(3));
			}
	}