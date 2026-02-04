using Mikita.Nulls;
using System;

namespace Mikita.Structs.Perhaps;

public sealed class Maybe<T>
	(
		Func<T?> value
	)
	: IMaybe<T>
	{
		public T Current
			=> value().NotNull();

		public bool IsSome
			=> value() is not null;
	}