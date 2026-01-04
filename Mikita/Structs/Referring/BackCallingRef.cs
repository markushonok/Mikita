using System;

namespace Mikita.Structs.Referring;

public sealed class BackCallingRef<T>
	(
		IRef<T> source,
		Action callback
	)
	: IRef<T>
	{
		public T Value
			{
				get => source.Value;
				set => BackCallingAssign(value);
			}

		private void BackCallingAssign(T value)
			{
				source.Value = value;
				callback();
			}
	}