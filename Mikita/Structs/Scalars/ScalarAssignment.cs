using Mikita.Routines.Assignment;

namespace Mikita.Structs.Scalars;

public static class ScalarAssignment
	{
		extension<T>(Scalar<T> scalar)
			{
				public Assignment<T> Assignment
					=> new(value => scalar.Value = value);

				public void SetTo
					(
						T value
					)
					=> scalar.Value = value;
			}

	}