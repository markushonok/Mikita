using Mikita.Godot.Threading;
using Mikita.Observation.Events;
using Mikita.Threading;
using System;

namespace Mikita.Godot.Observation.Events;

/// <summary>
/// Defers an event source's raise to the main thread.
/// </summary>
/// <param name="source">Event source whose raises are deferred.</param>
public sealed class DeferredRaiseEvent<T>
	(
		IEventSource<T> source
	)
	: IEventSource<T>
	{
		/// <summary>
		/// Adds a reaction to the source.
		/// </summary>
		public void Add(T reaction)
			=> source.Add(reaction);

		/// <summary>
		/// Removes a reaction from the source.
		/// </summary>
		public void Remove(T reaction)
			=> source.Remove(reaction);

		/// <summary>
		/// Raises the source on the main thread.
		/// </summary>
		public void Raise(Action<T> pattern)
			=> Deferred
				.Do(() => source.Raise(pattern))
				.Forget();
	}