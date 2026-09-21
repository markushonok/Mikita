using Mikita.Observation.Events;

namespace Mikita.Godot.Observation.Events;

public static class DeferredRaiseEventInstancing
	{
		extension<T>(IEventSource<T> source)
			{
				/// <summary>
				/// Defers raises to the main thread. The source must no longer be
				/// raised directly.
				/// </summary>
				public IEventSource<T> WithDeferredRaise
					=> new DeferredRaiseEvent<T>(source);
			}
	}