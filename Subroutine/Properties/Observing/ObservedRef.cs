namespace Mikita.Subroutine.Properties.Observing;

public sealed class ObservedRef<T>
	(
		T value,
		Action changing,
		Action changed
	) 
	: ObservedProperty<T>
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

		public event Action Changing = changing;

		public event Action Changed = changed;
	}
	
public static class ObservedRef
	{
		public static ObservedRef<T> To<T>
			(
				T referent
			)
			=> new
				(
					referent,
					changing: delegate {},
					changed: delegate {}
				);
	}