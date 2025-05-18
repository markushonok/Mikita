using System.Numerics;

namespace Mikita.Measurement.Lengths;

public readonly partial struct ValueLength<T>(T meters)
	: ILength<T>
	where T : struct, INumber<T>
	{
		public T Meters()
			=> meters;
	}