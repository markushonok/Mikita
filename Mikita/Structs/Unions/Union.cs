using System;

namespace Mikita.Structs.Unions;

public sealed class Union<T>
	(
		Action<T> pattern
	)
	: IUnion<T>
	{
		public void Switch(T handle)
			=> pattern(handle);
	}

public static class Union;