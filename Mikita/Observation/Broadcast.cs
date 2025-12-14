using Mikita.Generation.Broadcasting;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Mikita.Observation;

public static class Broadcast
	{
		public static T To<T>
			(
				IEnumerable<T> listeners
			)
			{
				try
					{
						return BroadcastRegistry.BroadcastTo(listeners);
					}
				catch (Exception exception)
					{
						throw NoGeneratorException(exception);
					}
			}

		public static NotSupportedException NoGeneratorException
			(
				Exception inner
			)
			=> new
				(
					"Make sure the Mikita.Generation source"
					+ " generator is referenced as an analyzer.",
					inner
				);
	}