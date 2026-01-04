using Mikita.Routines.Assignment;

namespace Mikita.Structs.Referring;

public static class RefAssignment
	{
		extension<T>(IRef<T> reference)
			{
				public Assignment<T> Assignment
					=> new(value => reference.Value = value);

				public void SetTo
					(
						T value
					)
					=> reference.Value = value;
			}
	}