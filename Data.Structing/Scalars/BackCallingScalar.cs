namespace Mikita.Many.Scalars;

public sealed class BackCallingScalar<T>
	(
		Scalar<T> origin,
		Action callback
	)
	: Scalar<T>
	{
		public T Value
			{
				get => origin.Value;
				set => BackCallingAssign(value);
			}

		private void BackCallingAssign(T value)
			{
				origin.Value = value;
				callback();
			}
	}