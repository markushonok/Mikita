namespace Mikita.Measurement.Lengths;

public readonly partial struct ValueLength<T>
	{
		public static ValueLength<T> Zero { get; }
			= new(T.Zero);
	}