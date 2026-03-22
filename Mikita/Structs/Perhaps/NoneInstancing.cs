namespace Mikita.Structs.Perhaps;

public static class NoneInstancing
	{
		extension<T>(None<T>)
			{
				public static None<T> Instance
					=> NoneInstancing<T>.Instance;
			}
	}

file static class NoneInstancing<T>
	{
		public static readonly None<T> Instance
			= new();
	}