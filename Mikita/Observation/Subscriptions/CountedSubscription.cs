using Mikita.Structs.Referring;
using System;

namespace Mikita.Observation.Subscriptions;

public sealed class CountedSubscription
	(
		ISubscription source,
		IRef<int> number
	)
	: ISubscription
	{
		public void Activate()
			{
				number.Value++;
				if (number.Value == 1) source.Activate();
			}

		public void Deactivate()
			{
				number.Value--;

				if (number.Value == 0)
					{
						source.Deactivate();
					}
				else if (number.Value < 0)
					throw NegativeCountException(number.Value);
			}

		private static Exception NegativeCountException
			(
				int count
			)
			=> new InvalidOperationException
				(
					$"Subscription count dropped to {count}."
					+ $" `Deactivate` was called more times than `Activate`."
				);
	}