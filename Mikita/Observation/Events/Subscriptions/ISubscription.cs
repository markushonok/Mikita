namespace Mikita.Observation.Events.Subscriptions;

public interface ISubscription
	{
		void Activate();

		void Deactivate();
	}