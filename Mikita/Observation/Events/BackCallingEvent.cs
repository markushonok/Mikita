using Mikita.Observation.Events.Managed;
using System;

namespace Mikita.Observation.Events;

public sealed class BackCallingEvent<T>
	(
		IManagedEvent<T> source,
		Action raiseCallback
	)
	: IManagedEvent<T>
	{

		public void Add(T reaction)
			=> source.Add(reaction);

		public void Remove(T reaction)
			=> source.Remove(reaction);

		public void Raise(Action<T> arouse)
			{
				source.Raise(arouse);
				raiseCallback();
			}
	}