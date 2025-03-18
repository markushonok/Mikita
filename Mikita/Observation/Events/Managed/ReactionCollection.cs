using System.Collections.Generic;

namespace Mikita.Observation.Events.Managed;

public sealed class ReactionCollection<T>
	(
		T invoke,
		ICollection<T> reactions
	) 
	: ManagedEvent<T>
	{
		public T Invoke
			=> invoke;

		public void Add(T reaction)
			=> reactions.Add(reaction);

		public void Remove(T reaction)
			=> reactions.Remove(reaction);
	}