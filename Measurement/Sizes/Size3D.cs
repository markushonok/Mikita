using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public interface Size3D<out T> where T : INumber<T>
	{
		Length<T> Width { get; }
		
		Length<T> Height { get; }
		
		Length<T> Depth { get; }
	}