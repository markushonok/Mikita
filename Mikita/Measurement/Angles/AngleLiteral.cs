using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleLiteral
	{
		extension<T>(T number)
			where T: INumber<T>, IFloatingPointConstants<T>
			{
				/// <summary>
				/// Literal for radians.
				/// </summary>
				public AngleRecord<T> rad
					=> Angle.Radians(number);

				/// <summary>
				/// Literal for degrees.
				/// </summary>
				public AngleRecord<T> deg
					=> Angle.Degrees(number);

				/// <summary>
				/// Literal for turns.
				/// </summary>
				public AngleRecord<T> turns
					=> Angle.Turns(number);
			}
	}