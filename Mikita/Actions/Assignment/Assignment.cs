using System;

namespace Mikita.Actions.Assignment;

public class Assignment<T>
	(
		Action<T> action
	)
	: IAssignment<T>
	{
		public void SetTo(T value)
			=> action(value);
	}