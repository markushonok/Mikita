namespace Mikita.Observing;

public sealed class ArtificialSource<T>
	(
		T value,
		Action changing,
		Action changed
	) 
	: ObservedSource<T>
	{
		public T Value
			{
				get => value;
				set
					{
						Changing();
						this.value = value;
						Changed();
					}
			}

		private T value = value;

		public event Action Changing 
			= changing;

		public event Action Changed 
			= changed;
	}