using System;
using System.Collections.Generic;

namespace Mikita.Generation.Broadcasting;

public static class BroadcastRegistry
	{
		public static T BroadcastTo<T>
			(
				IEnumerable<T> listeners
			)
			{
				var factory = (Func<IEnumerable<T>, T>) Factories[typeof(T)];
				return factory(listeners);
			}

		public static void Add<T>
			(
				Func<IEnumerable<T>, T> factory
			)
			=> Factories.Add(typeof(T), factory);

		private static readonly Dictionary<Type, object> Factories
			= new();
	}