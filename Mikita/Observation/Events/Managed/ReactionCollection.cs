using System.Collections.Generic;

namespace Mikita.Observation.Events.Managed;

public sealed partial class ReactionCollection<T>
	(
		ICollection<T> reactions,
		T invoke
	) 
	: ManagedEvent<T>
	{
		public void Add(T reaction)
			=> reactions.Add(reaction);

		public void Remove(T reaction)
			=> reactions.Remove(reaction);

		public T Invoke
			=> invoke;
	}