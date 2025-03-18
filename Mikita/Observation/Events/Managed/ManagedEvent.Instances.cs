using System;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Managed;

public static class ManagedEvent
	{
		public static ReactionCollection<Action> Empty
			=> ManagedEvent.With([]);

		public static ReactionCollection<Action> With
			(
				Action reaction
			)
			=> ManagedEvent.With([reaction]);

		public static ReactionCollection<Action> With
			(
				ICollection<Action> reactions
			)
			=> new
				(
					() =>
						{
							foreach (var reaction in reactions)
								reaction();
						},
					reactions
				);
	}