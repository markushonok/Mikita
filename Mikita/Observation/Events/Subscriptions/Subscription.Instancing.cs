using Mikita.Steps;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Observation.Events.Subscriptions;

partial class Subscription
	{
		public static Subscription Of
			(
				IEnumerable<ISubscription> subscriptions
			)
			=> Subscription.From(subscriptions.Select(x => x.AsStep));

		public static Subscription From
			(
				IEnumerable<IStep> steps
			)
			=> Subscription.From(Walk.Of(steps));

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