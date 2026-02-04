using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Extensions;

public sealed class ExtendedEvent<TBase, T>
	(
		IEvent<TBase> source,
		ICollection<T> extension
	)
	: IEventSource<T>
	where T: TBase
	{
		public void Add(T reaction)
			{
				source.Add(reaction);
				extension.Add(reaction);
			}

		public void Remove(T reaction)
			{
				source.Remove(reaction);
				extension.Remove(reaction);
			}

		public void Raise(Action<T> pattern)
			=> extension.ForEach(pattern);
	}