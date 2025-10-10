using Mikita.Actions.Assignment;
using System;

namespace Mikita.Structs.Scalars;

public sealed class RelayScalar<T>
	(
		Func<T> @return,
		Assign<T> assign
	)
	: Scalar<T>
	{
		public T Value
			{
				get => @return();
				set => assign(value);
			}
	}