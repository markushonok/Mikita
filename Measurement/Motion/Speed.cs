using System.Numerics;

namespace Mikita.Measurement.Motion;

public partial interface Speed<out T>
	where T : INumber<T>, IRootFunctions<T>
	{
		T InMetersPerSecond { get; }
	}