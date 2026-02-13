using Mikita.Math.Indices;
using Mikita.Math.Sizes;
using Mikita.Structs.Referring;

namespace Mikita.Structs.Arrays;

public static class ArrayResizing
	{
		extension<T>(IRef<IArray2D<T?>> reference)
			{
				public void ResizeTo
					(
						ISize2D<int> size
					)
					=> reference.Value = reference.Value.ResizedTo(size);
			}

		extension<T>(IArray2D<T> source)
			{
				public IArray2D<T?> ResizedTo
					(
						ISize2D<int> size
					)
					{
						var result = Array.NewOf<T>(size);
						var intersection = Size.IntersectionOf(source.Size, result.Size);
						var indices = Index.EachIn(intersection);

						foreach (var index in indices)
							result[index] = source[index];

						return result;
					}
			}
	}