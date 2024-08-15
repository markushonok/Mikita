namespace Mikita.Subroutine.Properties;

public sealed class Ref<T> : Property<T>
	{
		public required T Value { get; set; }
	}
	
public static class Ref
	{
		public static Ref<T> To<T>
			(
				T referent
			)
			=> new() {Value = referent};
	} 