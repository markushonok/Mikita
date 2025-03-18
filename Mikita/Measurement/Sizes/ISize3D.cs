using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public interface ISize3D<out T> where T : INumber<T>
	{
		ILength<T> Width { get; }
		
		ILength<T> Height { get; }
		
		ILength<T> Depth { get; }
	}