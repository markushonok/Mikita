using System;

namespace Mikita.Observation.Events.Subscriptions;

public static class SubscriptionInstancing
	{
		extension(ISubscription source)
			{
				public Subscription PreparingBy
					(
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

				public Subscription ReleasedBy
					(
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
			}

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