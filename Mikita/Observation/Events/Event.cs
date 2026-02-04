using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events;

public sealed class Event<T>
	(
		ICollection<T> reactions
	) 
	: IEventSource<T>
	{
		public void Add(T reaction)
			=> reactions.Add(reaction);

		public void Remove(T reaction)
			=> reactions.Remove(reaction);

		public void Raise(Action<T> pattern)
			=> reactions.ForEach(pattern);
	}

public static class Event;