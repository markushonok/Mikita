using System;

namespace Mikita.Structs.Perhaps;

public static class MaybeDoing
	{
		extension<T>(IMaybe<T> maybe)
			{
				public void Do(Action<T> action)
					{
						if (maybe.IsSome)
							action(maybe.Current);
					}
			}
	}