using System;

namespace Mikita.Observation.Change;

public sealed class ObservedSource<T>
	(
		T source,
		Action changing,
		Action changed
	) 
	: Interserved<T>
	{
		public T Current
			{
				get => current;
				set
					{
						Changing();
						current = value;
						Changed();
					}
			}

		private T current = source;

		public event Action Changing 
			= changing;

		public event Action Changed 
			= changed;
	}