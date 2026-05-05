using System;

namespace Mikita.Structs.Unions;

public static class UnionInstancing
	{
		extension<T>(Union)
			{
				public static IUnion<T> That
					(
						Action<T> @do
					)
					=> new Union<T>(@do);
			}
	}