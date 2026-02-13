using System;

namespace Mikita.Structs.Cursors;

public sealed class CallbackCursor<T>
	(
		ICursor<T> target,
		Action<T> add,
		Action remove
	)
	: ICursor<T>
	{
		public T Current
			=> target.Current;

		public bool IsSome
			=> target.IsSome;

		public void Add(T item)
			{
				target.Add(item);
				add(item);
			}

		public void Remove()
			{
				target.Remove();
				remove();
			}
	}