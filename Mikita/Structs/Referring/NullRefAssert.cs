using Mikita.Nulls;

namespace Mikita.Structs.Referring;

public static class NullRefAssert
	{
		extension<T>(IRef<T?> reference)
			{
				public IRef<T> NotNull()
					=> Ref.That
						(
							returns: () => reference.Value.NotNull(),
							assigns: value => reference.SetTo(value.NotNull())
						);
			}
	}