using System;

namespace Mikita.Objects.Tokens;

public readonly struct MemberToken<T>
	(
		Func<T, object?> value,
		string name
	)
	{
		public string Name
			=> name;

		public object? ValueOf(T instance)
			=> value(instance);
	}