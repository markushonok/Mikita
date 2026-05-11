using Mikita.Observation.Events;
using Mikita.Routines;
using Mikita.Threading.Pools;
using System;

namespace Mikita.Observation.Subscriptions;

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

		public static ISubscription SubscriptionOf
			(
				this IEvent<Action> @event,
				ITaskPool tasks,
				CancellableTask reaction
			)
			=> @event.SubscriptionOf
				(
					() => tasks.Put(reaction)
				);

		public static ISubscription SubscriptionOf<T>
			(
				this IEvent<T> @event,
				Func<T> reaction
			)
			=> new Subscription
				(
					activate: () => @event.Add(reaction()),
					deactivate: () => @event.Remove(reaction())
				);

		public static ISubscription SubscriptionOf<T>
			(
				this IEvent<T> @event,
				T reaction
			)
			=> new Subscription
				(
					activate: () => @event.Add(reaction),
					deactivate: () => @event.Remove(reaction)
				);
	}