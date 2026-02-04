using Mikita.Observation.Change;
using Mikita.Structs.Perhaps;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.States.Orders;

public sealed class StateOrder<T>
	(
		IEnumerable<IStateOrderItem<T>> items
	)
	: IMaybe<T>
	{
		public T Current
			{
				get
					{
						var item = items.FirstOrDefault(x => x.IsActive);

						return item == null
							? throw StateOrder.NoActiveItemException
							: item.Key;
					}
			}

		public bool IsSome
			=> items.Any(x => x.IsActive);
	}

public static class StateOrder;