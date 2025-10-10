using Mikita.Actions.Assignment;

namespace Mikita.Structs.Scalars;

public static class ScalarAssignment
	{
		public static IAssignment<T> Assignment<T>
			(
				this Scalar<T> scalar
			)
			=> new Assignment<T>(value => scalar.Value = value);

		public static void SetTo<T>
			(
				this Scalar<T> scalar,
				T value
			)
			=> scalar.Value = value;
	}