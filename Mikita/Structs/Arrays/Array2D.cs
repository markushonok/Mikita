using Mikita.Math.Indices;
using Mikita.Math.Sizes;
using System;
using Index = Mikita.Math.Indices.Index;

namespace Mikita.Structs.Arrays;

public sealed class Array2D<T>
	(
		T[] source,
		ISize2D<int> size
	)
	: IArray2D<T>
	{
		public ISize2D<int> Size
			=> size;

		public T this[IIndex2D index]
			{
				get => source[Inlined(index)];
				set => source[Inlined(index)] = value;
			}

		private int Inlined(IIndex2D index)
			=> index.InlineTo(size);
	}