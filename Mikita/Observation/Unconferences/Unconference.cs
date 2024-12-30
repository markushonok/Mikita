using System;

namespace Mikita.Observation.Unconferences;

public interface Unconference<out T>
	{
		event Action Happened;

		T Invoke { get; }
	}