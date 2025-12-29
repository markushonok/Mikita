using System;

namespace Mikita.Routines.Assignment;

public sealed class Assignment<T>
	(
		Action<T> action
	)
	: IAssignment<T>
	{
		public void SetTo(T value)
			=> action(value);
	}