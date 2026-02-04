using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Adaptation;

public static class EventAdaptation
	{
		extension<TFrom, TTo>(IEvent<TFrom> source)
			where TFrom : Delegate
			where TTo : Delegate
			{
				public AdaptedEvent<TFrom, TTo> Adapted
					(
						Func<TTo, TFrom> wrap
					)
					=> new
						(
							source,
							wrap,
							new Dictionary<TTo, TFrom>()
						);
			}
	}