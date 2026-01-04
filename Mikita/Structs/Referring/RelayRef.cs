using Mikita.Routines.Assignment;
using System;

namespace Mikita.Structs.Referring;

public sealed class RelayRef<T>
	(
		Func<T> @return,
		Assign<T> assign
	)
	: IRef<T>
	{
		public T Value
			{
				get => @return();
				set => assign(value);
			}
	}