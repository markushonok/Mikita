using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class NumberLiterals
	{
		extension<T>(T number)
			where T: struct, INumber<T>
			{
				public ValueLength<T> km
					=> new(meters: number * T.CreateChecked(1000));

				public ValueLength<T> m
					=> new(meters: number);

				public ValueLength<T> cm
					=> new(meters: number / T.CreateChecked(100));

				public ValueLength<T> mm
					=> new(meters: number / T.CreateChecked(1000));
			}
	}
