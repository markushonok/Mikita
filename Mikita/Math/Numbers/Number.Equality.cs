using System.Numerics;

namespace Mikita.Math.Numbers;

public static partial class Number
	{
		public static bool IsApprox<T>
			(
				this T left,
				T right
			)
			where T : INumber<T>, IFloatingPointIeee754<T>
			=> left.IsApprox(right, T.Epsilon);

		public static bool IsApprox<T>
			(
				this T left,
				T right,
				T tolerance
			)
			where T : INumber<T>
			=> T.Abs(left - right) < tolerance;
	}