namespace Mikita.Measurement.Masses;

public readonly struct ValueMass<T>(T kilograms)
	: IMass<T> where T : struct
	{
		public T Kilograms() => kilograms;
	}