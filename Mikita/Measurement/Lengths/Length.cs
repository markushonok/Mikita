using System;
using System.Numerics;

namespace Mikita.Measurement.Lengths;

public sealed partial class Length<T>(T meters)
	: ILength<T>, IEquatable<ILength<T>>
	where T: INumber<T>
	{
		public T Meters()
			=> meters;

		public override string? ToString()
			=> meters.ToString();
	}