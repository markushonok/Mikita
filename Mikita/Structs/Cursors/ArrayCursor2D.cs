using Mikita.Math.Indices;
using Mikita.Nulls;
using Mikita.Structs.Arrays;
using Mikita.Structs.Perhaps;
using Mikita.Structs.Referring;
using System;

namespace Mikita.Structs.Cursors;

public sealed class ArrayCursor2D<T>
	(
		IRef<IArray2D<T?>> array,
		IIndex2D index
	)
	: ICursor<T>
	{

		public T Current
			=> array.Value[index].NotNull();

		public bool IsSome
			=> array.Value[index] is not null;

		public void Add(T item)
			{
				if (IsSome) throw OccupiedCellException;
				array.Value[index] = item;
			}

		public void Remove()
			{
				if (this.IsNone) throw EmptyCellException;
				array.Value[index] = default;
			}

		private InvalidOperationException OccupiedCellException
			=> new($"Cell at {index} is already occupied.");

		private InvalidOperationException EmptyCellException
			=> new($"Cell at {index} is empty.");
	}