using System;

namespace Mikita.Structs.Perhaps;

public sealed class None<T>: IMaybe<T>
	{
		public T Current
			=> throw new Exception();

		public bool IsSome
			=> false;
	}