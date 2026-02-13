using Mikita.Math.Indices;
using TSize = Mikita.Math.Sizes.Size;

namespace Mikita.Structs.Arrays;

public interface IArray2D<T>
	: IReadOnlyArray2D<T>, IWriteOnlyArray2D<T>
	{
		new T this[IIndex2D index] { get; set; }
	}