using Mikita.Subroutine.Funcs;

namespace Mikita.Subroutine.Props;

public interface Prop<T> : ReadOnlyProp<T>, WriteOnlyProp<T>
	{
		new T Value { get; set; }
	}

public static class Prop
	{
		public static PropRecord<T> Of<T>(T value) => new(value);
		
		public static RelayProp<T> Of<T>
			(
				Func<T> accessor,
				Action<T> mutator
			)
			=> new(accessor, mutator);

		public static ReactiveProp<T> Of<T>
			(
				T value,
				Action callback
			)
			=> new(Prop.Of(value), callback);

		public static ReactiveProp<T> Of<T>
			(
				Prop<T> prop,
				Action callback
			)
			=> new(prop, callback);
	}