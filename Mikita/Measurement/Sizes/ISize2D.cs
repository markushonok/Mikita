using Mikita.Measurement.Lengths;
using System.Numerics;

namespace Mikita.Measurement.Sizes;

public interface ISize2D<out T> where T : INumber<T>
	{
		ILength<T> Width { get; }
		
		ILength<T> Height { get; }
	}