using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Adaptation;

public sealed class AdaptedEvent<TFrom, TTo>
	(
		IEvent<TFrom> source,
		Func<TTo, TFrom> wrap,
		IDictionary<TTo, TFrom> map
	)
	: IEvent<TTo> where TTo: notnull
	{
		public void Add(TTo reaction)
			{
				var wrapped = wrap(reaction);
				map[reaction] = wrapped;
				source.Add(wrapped);
			}

		public void Remove(TTo reaction)
			{
				source.Remove(map[reaction]);
				map.Remove(reaction);
			}
	}