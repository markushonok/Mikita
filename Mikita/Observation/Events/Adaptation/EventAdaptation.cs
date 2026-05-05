using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Adaptation;

public static class EventAdaptation
	{
		extension<TFrom, TTo>
			(
				IEvent<TFrom> source
			)
			where TFrom: Delegate
			where TTo: Delegate
			{
				public IEvent<TTo> Adapted
					(
						EventWrapPattern<TFrom, TTo> wrap
					)
					=> new AdaptedEvent<TFrom, TTo>
						(
							source,
							wrap,
							new Dictionary<TTo, TFrom>()
						);
			}
	}