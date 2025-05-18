using System.Collections.Generic;

namespace Mikita.States.PriorityOrders;

public static class PriorityOrder
	{
		public static StateEnumerable<T> With<T>
			(
				IEnumerable<IStateOrderItem<T>> items
			)
			=> new(items);
	}