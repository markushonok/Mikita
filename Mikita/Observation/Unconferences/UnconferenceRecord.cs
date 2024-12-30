using System;

namespace Mikita.Observation.Unconferences;

public sealed class UnconferenceRecord<T>
	(
		T invoke,
		Action happened
	) 
	: Unconference<T>
	{
		public T Invoke => invoke;

		public event Action Happened 
			= happened;
	}