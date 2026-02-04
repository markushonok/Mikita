using System;

namespace Mikita.States.Orders;

public sealed class StateOrderItem<T>
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

public static class StateOrderItem;