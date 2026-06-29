using System;

namespace Mikita.Objects.Tokens;

public static class ObjectTokenInstancing
	{
		extension<T>(ObjectToken) where T: notnull
			{
				public static ObjectToken<T> Of
					(
						T source,
						ReadOnlySpan<MemberToken<T>> members
					)
					=> new(source, members);
			}
	}