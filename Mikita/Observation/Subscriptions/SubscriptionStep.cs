using Mikita.Steps;

namespace Mikita.Observation.Subscriptions;

public static class SubscriptionStep
	{
		extension(ISubscription subscription)
			{
				public AsyncStep AsAsyncStep
					=> subscription.AsStep.AsAsync;

				public Step AsStep
					=> new
						(
							subscription.Activate,
							subscription.Deactivate
						);
			}
	}