using Mikita.Math.Indices;

namespace Mikita.Structs.Arrays;

public interface IWriteOnlyArray2D<in T>
	: ISized2D<int>
	{
		T this[IIndex2D index] { set; }
	}