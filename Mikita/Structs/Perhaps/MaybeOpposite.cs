namespace Mikita.Structs.Perhaps;

public static class MaybeOpposite
	{
		extension<T>(IMaybe<T> value)
			{
				public bool IsNone
					=> !value.IsSome;
			}
	}