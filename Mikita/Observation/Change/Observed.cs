using System;

namespace Mikita.Observation.Change;

public interface Observed<out T>
	{
		event Action Changed;

		T Current { get; }
	}