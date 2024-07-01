namespace Mikita.Subroutine.Props;

public class NestedReactiveProp<T>
	(
		Prop<T> origin,
		Action changed
	) : ReactiveProp<T>
	{
		public T Value
			{
				get => origin.Value;
				set
					{
						origin.Value = value;
						Changed();
					}
			}

		public event Action Changed = changed;
	}