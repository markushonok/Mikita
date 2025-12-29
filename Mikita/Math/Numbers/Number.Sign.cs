using System.Numerics;

namespace Mikita.Math.Numbers;

public static partial class Number
	{
		extension<T>(T number) where T: INumber<T>
			{
				public T TSign
					=> number >= T.Zero ? T.One : -T.One;
			}
	}