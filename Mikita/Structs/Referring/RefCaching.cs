namespace Mikita.Structs.Referring;

public static class RefCaching
	{
		extension<T>(IRef<T> reference)
			{
				public IRef<T> Snapshot
					=> Ref.To(reference.Value);
			}
	}