using System.Numerics;

namespace Mikita.Measurement.Lengths;

public partial interface Length<out T>
	{
		public static LengthRecord<T> Zero
			=> new(T.Zero);
	}

public static class Length
	{
		public static LengthRecord<T> InMeters<T>(T number) 
			where T : INumber<T>
			=> new(number);
	}