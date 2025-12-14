using Mikita.Steps;
using System.Collections.Generic;

namespace Mikita.Observation.Events.Subscriptions;

partial class Subscription
	{
		public static Subscription From
			(
				IEnumerable<IStep> steps
			)
			=> From(Walk.Of(steps));

		public static Subscription From
			(
				IStep step
			)
			=> new
				(
					step.Do,
					step.Undo
				);
	}