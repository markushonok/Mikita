using Mikita.Observation.Events.Managed;
using Mikita.Structs.Enumerables;
using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events;

public static class Event
	{
		public static EventVerbatim<T> WithPattern<T>
			(
				Action<T> add,
				Action<T> remove
			)
			=> new(add, remove);

		public static ReactionCollection<Action> Empty
			=> With([]);

		public static ReactionCollection<Action> With
			(
				Action reaction
			)
			=> With([reaction]);

		public static ReactionCollection<Action> With
			(
				ICollection<Action> reactions
			)
			=> new
				(
					reactions,
					() => reactions.ForEachDo(r => r())
				);

		public static ReactionCollection<T> With<T>
			(
				ICollection<T> reactions,
				T invoke
			)
			=> new
				(
					reactions,
					invoke
				);
	}