using Mikita.Objects.Tokens;
using NetFabric.Hyperlinq;
using System;

namespace Mikita.Objects;

public static class ObjectComparison
	{
		public static bool NotEquals
			(
				this object a,
				object? b
			)
			=> !a.Equals(b);

		extension<T>(T? left)
			{
				public bool EqualsBy
					(
						T? right,
						params ReadOnlySpan<MemberToken<T>> members
					)
					=> ReferenceEquals(left, right)
						|| left is not null
						&& right is not null
						&& members
							.AsValueEnumerable()
							.All(x => left.EqualsBy(right, x.ValueOf));
			}

		extension<T>(T left)
			{
				public bool EqualsBy
					(
						T right,
						Func<T, object?> member
					)
					=> Equals(member(left), member(right));
			}
	}