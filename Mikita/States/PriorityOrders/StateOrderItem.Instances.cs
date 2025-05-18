using System;

namespace Mikita.States.PriorityOrders;

public static class PriorityItem
	{
		public static StateOrderItem<T> Default<T>(T key)
			=> new
				(
					() => key,
					isActive: () => true
				);

		public static StateOrderItem<T> With<T>
			(
				T key,
				Func<bool> activeWhen
			)
			=> new(() => key, activeWhen);
	}