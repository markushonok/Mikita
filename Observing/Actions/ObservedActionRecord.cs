namespace Mikita.Observing.Actions;

public sealed class ObservedActionRecord<T>
	(
		T invoke,
		Action happening,
		Action happened
	) 
	: ObservedAction<T>
	{
		public T Invoke => invoke;
		
		public event Action Happening 
			= happening;

		public event Action Happened 
			= happened;
	}