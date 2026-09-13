using Mikita.Logging;
using System;

namespace Mikita.Observation.Events;

/// <summary>
/// Continues raising an event after logging a failed reaction.
/// </summary>
public sealed class RaiseSafeEvent<T>
	(
		IEventSource<T> source,
		ILog log
	)
	: IEventSource<T>
	{
		public void Add(T reaction)
			=> source.Add(reaction);

		public void Remove(T reaction)
			=> source.Remove(reaction);

		public void Raise(Action<T> pattern)
			=> source.Raise(reaction => Raise(pattern, reaction));

		private void Raise
			(
				Action<T> pattern,
				T reaction
			)
			{
				try
					{
						pattern(reaction);
					}
				catch (Exception exception)
					{
						log.Write(exception.ToString());
					}
			}
	}