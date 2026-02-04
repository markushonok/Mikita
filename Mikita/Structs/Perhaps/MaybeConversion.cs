using Mikita.Structs.Referring;

namespace Mikita.Structs.Perhaps;

public static class MaybeConversion
	{
		extension<T>(IRef<T?> reference)
			{
				public Maybe<T> AsMaybe
					=> new(reference.AsFunction);
			}
	}