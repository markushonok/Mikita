using System.Numerics;

namespace Mikita.Measurement.Masses;

public static class MassLiterals
	{
		extension<T>(T number) where T : struct, INumber<T>
			{
				public ValueMass<T> t
					=> new(kilograms: number * T.CreateChecked(1000));

				public ValueMass<T> kg
					=> new(kilograms: number);

				public ValueMass<T> g
					=> new(kilograms: number / T.CreateChecked(1000));
			}

	}