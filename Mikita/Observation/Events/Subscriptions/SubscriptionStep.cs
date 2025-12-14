using Mikita.Steps;

namespace Mikita.Observation.Events.Subscriptions;

public static class SubscriptionStep
	{
		public static Step AsStep
			(
				this ISubscription subscription
			)
			=> new
				(
					subscription.Activate,
					subscription.Deactivate
				);
	}