namespace Mikita.Observing;

public static class EventToggle
	{
		public static Func<bool> From
			(
				Action<Action> subscribeToPositive,
				Action<Action> subscribeToNegative
			)
			{
				var state = false;
				subscribeToPositive(() => state = true);
				subscribeToNegative(() => state = false);
				return () => state;
			}
	}