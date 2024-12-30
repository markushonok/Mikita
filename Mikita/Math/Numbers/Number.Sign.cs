using System.Numerics;

namespace Mikita.Math.Numbers;

public static partial class Number
	{
		public static T Sign<T>
			(
				this T number
			) 
			where T: INumber<T>
			=> number >= T.Zero ? T.One : -T.One;
	}