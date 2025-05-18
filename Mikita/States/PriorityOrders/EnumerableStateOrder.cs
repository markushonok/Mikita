using System;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.States.PriorityOrders;

public class StateEnumerable<T>
	(
		IEnumerable<IStateOrderItem<T>> items
	)
	: StateOrder<T>
	{
		public T Current
			=> FindCurrent();

		private T FindCurrent()
			{
				try
					{
						return items.Last(x => x.IsActive).Key;
					}
				catch(InvalidOperationException exception)
					{
						throw new InvalidOperationException(
							"Priority order doesn't have any one active item",
							exception);
					}
			}
	}