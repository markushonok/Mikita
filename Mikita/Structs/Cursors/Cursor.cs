using System;

namespace Mikita.Structs.Cursors;

public sealed class Cursor<T>
	(
		Func<T> current,
		Func<bool> has,
		Action<T> add,
		Action remove
	)
	: ICursor<T>
	{
		public T Current
			=> current();

		public bool IsSome
			=> has();

		public void Add(T item)
			=> add(item);

		public void Remove()
			=> remove();
	}