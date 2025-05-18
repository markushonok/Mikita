using System;

namespace Mikita.States.PriorityOrders;

public sealed partial class StateOrderItem<T>
	(
		Func<T> key,
		Func<bool> isActive
	)
	: IStateOrderItem<T>
	{
		public T Key
			=> key();

		public bool IsActive
			=> isActive();
	}