using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Sources;

public sealed partial class ReactionCollection<T>
	(
		ICollection<T> reactions
	) 
	: IEventSource<T>
	where T: class
	{
		public void Add(T reaction)
			=> reactions.Add(reaction);

		public void Remove(T reaction)
			=> reactions.Remove(reaction);

		public T Listener { get; }
			= Broadcast.To(reactions);
	}