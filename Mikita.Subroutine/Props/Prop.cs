namespace Mikita.Subroutine.Props;

public interface Prop<T> : ReadOnlyProp<T>, WriteOnlyProp<T>
	{
		new T Value { get; set; }
	}

public static class Prop
	{
		public static PropRecord<T> Of<T>(T value)
			=> new(value);
		
		public static ReactiveProp<T> Of<T>
			(
				T value,
				Action change
			)
			=> new(Prop.Of(value), change);

		public static ReactiveProp<T> Of<T>
			(
				Prop<T> prop,
				Action change
			)
			=> new(prop, change);
	}