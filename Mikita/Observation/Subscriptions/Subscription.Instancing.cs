using Mikita.Steps;
using Mikita.Steps.Walking;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Observation.Subscriptions;

partial class Subscription
	{
		public static Subscription Of
			(
				IEnumerable<ISubscription> subscriptions
			)
			=> From(subscriptions.Select(x => x.AsStep));

		public static Subscription From
			(
				IEnumerable<IStep> steps
			)
			=> From(Walk.Of(steps));

		public static Subscription From
			(
				IStep step
			)
			=> new(step.Do, step.Undo);

		public static ISubscription Idle { get; }
			= new Subscription
				(
					activate: delegate {},
					deactivate: delegate {}
				);
	}