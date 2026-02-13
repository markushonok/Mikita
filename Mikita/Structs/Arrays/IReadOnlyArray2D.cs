using Mikita.Math.Indices;

namespace Mikita.Structs.Arrays;

public interface IReadOnlyArray2D<out T>
	: ISized2D<int>
	{
		T this[IIndex2D index] { get; }
	}