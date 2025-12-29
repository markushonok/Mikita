using System.Numerics;

namespace Mikita.Measurement.Lengths;

public static class LengthToNumber
	{
		extension<T>(ILength<T> length)
			where T: INumber<T>
			{
				public T Kilometers
					=> length.Meters * T.CreateChecked(0.001);

				public T Centimeters
					=> length.Meters * T.CreateChecked(100);

				public T Millimeters
					=> length.Meters * T.CreateChecked(1000);
			}

	}