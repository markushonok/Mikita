using System.Collections.Generic;

namespace Mikita.Observation.Events.Extensions;

public static class EventExtension
	{
		extension<TBase>(IEvent<TBase> source)
			{
				public ExtendedEvent<TBase, T> ExtendedBy<T>
					(
						ICollection<T> extension
					)
					where T: TBase
					=> new(source, extension);
			}
	}