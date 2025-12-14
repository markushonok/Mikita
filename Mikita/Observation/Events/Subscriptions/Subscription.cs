using System;

namespace Mikita.Observation.Events.Subscriptions;

public sealed partial class Subscription
	(
		Action activate,
		Action deactivate
	)
	: ISubscription
	{
		public void Activate()
			=> activate();

		public void Deactivate()
			=> deactivate();
	}