using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Mikita.Objects.Tokens;

public readonly ref struct ObjectToken<T>
	(
		T source,
		ReadOnlySpan<MemberToken<T>> members
	)
	where T: notnull
	{
		public override bool Equals
			(
				object? @object
			)
			=> @object is T other
				&& source.Equals(other);

		public bool Equals
			(
				T? @object
			)
			=> source.EqualsBy(@object, members);

		public override int GetHashCode()
			{
				var hash = new HashCode();

				foreach (var member in members)
					hash.Add(member.ValueOf(source));

				return hash.ToHashCode();
			}

		public override string ToString()
			{
				var builder = new StringBuilder();
				builder.Append(typeof(T).Name).Append(" { ");

				for (var i = 0; i < members.Length; i++)
					{
						var member = members[i];
						var value = member.ValueOf(source);

						builder.Append(member.Name).Append(": ");
						builder.Append(Formatted(value));

						if (i < members.Length - 1)
							builder.Append(", ");
					}

				builder.Append(" }");
				return builder.ToString();
			}

		private static string Formatted
			(
				object? value
			)
			=> value switch
				{
					null => "null",
					string x => $"\"{x}\"",
					IEnumerable x => Formatted(x),
					_ => value.ToString() ?? Formatted(value)
				};

		private static string Formatted
			(
				IEnumerable collection
			)
			{
				var elements = collection
					.Cast<object?>()
					.Select(Formatted)
					.ToList();

				return $"[{string.Join(", ", elements)}]";
			}

		private readonly ReadOnlySpan<MemberToken<T>> members = members;
	}

public static class ObjectToken;