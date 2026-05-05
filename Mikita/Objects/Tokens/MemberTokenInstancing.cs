using Mikita.Nulls;
using System;
using System.Runtime.CompilerServices;

namespace Mikita.Objects.Tokens;

public static class MemberTokenInstancing
	{
		extension<T>(T type)
			{
				public static MemberToken<T> Member
					(
						Func<T, object?> member,
						[CallerArgumentExpression("member")] string? expression = null
					)
					=> new
						(
							member,
							MemberNameFrom(expression.NotNull())
						);
			}

		private static string MemberNameFrom(string expression)
			{
				var parts = expression.Split('.');
				return parts.Length > 1 ? parts[^1] : expression;
			}
	}