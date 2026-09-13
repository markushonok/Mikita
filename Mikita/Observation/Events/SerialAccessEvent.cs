using System;
using System.Threading;

namespace Mikita.Observation.Events;

/// <summary>
/// Serializes access to another event source.
/// </summary>
/// <remarks>
/// All access to the source must pass through this instance.
/// </remarks>
public sealed class SerialAccessEvent<T>
	(
		IEventSource<T> source,
		Lock access
	)
	: IEventSource<T>
	{
		public void Add(T reaction)
			{
				lock (access) source.Add(reaction);
			}

		public void Remove(T reaction)
			{
				lock (access) source.Remove(reaction);
			}

		public void Raise(Action<T> pattern)
			{
				lock (access) source.Raise(pattern);
			}
	}