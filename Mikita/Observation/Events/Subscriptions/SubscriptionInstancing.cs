using System;

namespace Mikita.Observation.Events.Subscriptions;

public static class SubscriptionInstancing
	{
		public static Subscription PreparingBy
			(
				this ISubscription source,
				Action action
			)
			=> new
				(
					activate: () =>
						{
							action();
							source.Activate();
						},
					source.Deactivate
				);

		public static Subscription ReleasedBy
			(
				this ISubscription source,
				Action action
			)
			=> new
				(
					source.Activate,
					deactivate: () =>
						{
							action();
							source.Deactivate();
						}
				);

		public static Subscription SubscriptionOf<T>
			(
				this IEvent<T> @event,
				T reaction
			)
			=> new
				(
					activate: () => @event.Add(reaction),
					deactivate: () => @event.Remove(reaction)
				);
	}