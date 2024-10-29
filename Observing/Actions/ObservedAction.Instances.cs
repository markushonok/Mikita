namespace Mikita.Observing.Actions;

public partial interface ObservedAction
	{
		static ObservedActionRecord<T> Of<T>(T action)
			=> new
				(
					action,
					delegate {},
					delegate {}
				);

		static ObservedAction Of(Action action)
			=> ObservedAction.Of<Action>(action).Shorten();
	}