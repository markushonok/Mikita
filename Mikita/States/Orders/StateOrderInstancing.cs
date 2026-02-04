using System;
using System.Collections.Generic;

namespace Mikita.States.Orders;

public static class StateOrderInstancing
	{
		extension(StateOrder)
			{
				public static StateOrder<T> With<T>
					(
						IEnumerable<IStateOrderItem<T>> items
					)
					=> new(items);

				public static InvalidOperationException NoActiveItemException
					=> new("State order doesn't have any one active item");
			}
	}