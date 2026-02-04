using Mikita.Structs.Referring;
using System;

namespace Mikita.States.Orders;

public static class StateOrderItemInstancing
	{
		extension(StateOrderItem)
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
						IRef<bool> isActive
					)
					=> new(() => key, isActive.AsFunction);

				public static StateOrderItem<T> With<T>
					(
						T key,
						Func<bool> isActive
					)
					=> new(() => key, isActive);
			}
	}