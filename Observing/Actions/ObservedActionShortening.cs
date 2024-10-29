namespace Mikita.Observing.Actions;

public static class ObservedActionShortening
	{
		public static ObservedAction Shorten
			(
				this ObservedAction<Action> action
			)
			=> ObservedAction.AliasOf(action);
	}