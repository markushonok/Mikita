using Mikita.Math.Sizes;

namespace Mikita.Structs.Arrays;

public interface ISized2D<out T>
	{
		ISize2D<T> Size { get; }
	}