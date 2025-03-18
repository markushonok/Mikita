using System.Numerics;

namespace Mikita.Measurement.Lengths;

public sealed class Length<T>(T meters)
	: ILength<T>
	where T : INumber<T>
	{
		public T Meters()
			=> meters;
	}