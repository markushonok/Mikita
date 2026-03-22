namespace Mikita.Structs.Perhaps;

public static class MaybeInstancing
	{
		extension<T>(T @object)
			{
				public IMaybe<T> AsMaybe
					=> new Maybe<T>(() => @object);
			}
	}