namespace Mikita.Objects;

public static class ObjectCasting
	{
		public static T As<T>
			(
				this object source
			)
			=> (T) source;
	}