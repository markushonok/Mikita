using System;

namespace Mikita.Observation.Change;

public interface IObserved<out T>
	{
		event Action Changed;

		T Current { get; }
	}