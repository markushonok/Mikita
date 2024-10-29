namespace Mikita.Many.Scalars;

public sealed class Ref<T>
	{
		public required T Referent { get; set; }

		public RelayScalar<T> AsProperty
		
			=> RelayScalar.That
				(
					returns: () => Referent,
					assigns: value => Referent = value
				);
	}
	
public static class Ref
	{
		public static Ref<T> To<T>
			(
				T referent
			)
			=> new() {Referent = referent};
	} 