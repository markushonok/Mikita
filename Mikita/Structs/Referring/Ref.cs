namespace Mikita.Structs.Referring;

public sealed class Ref<T>
	(
		T referent
	)
	: IRef<T>
	{
		public T Value
			{
				get => referent;
				set => referent = value;
			}
	}

public static class Ref;