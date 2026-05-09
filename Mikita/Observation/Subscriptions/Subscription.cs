using System;

namespace Mikita.Observation.Subscriptions;

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