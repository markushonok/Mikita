using System.Numerics;

namespace Mikita.Measurement.Angles;

public static class AngleLiteral
	{
		/// <summary>
		/// Literal for radians.
		/// </summary>
		public static AngleRecord<T> rad<T>(this T number)

			where T
			: INumber<T>
			, IFloatingPointConstants<T>

			=> number.AsAngleInRadians();


		/// <summary>
		/// Literal for degrees.
		/// </summary>
		public static AngleRecord<T> deg<T>(this T number)

			where T
			: INumber<T>
			, IFloatingPointConstants<T>

			=> number.ToAngleInDegrees();


		/// <summary>
		/// Literal for turns.
		/// </summary>
		public static AngleRecord<T> turns<T>(this T number)

			where T
			: INumber<T>
			, IFloatingPointConstants<T>

			=> number.ToAngleInTurns();
	}