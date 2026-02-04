using Mikita.Routines.Assignment;
using System.Threading.Tasks;

namespace Mikita.Structs.Referring;

public static class RefAssignment
	{
		extension<T>(IRef<T> reference)
			{
				public Assignment<T> Assignment
					=> new(value => reference.Value = value);

				public async Task AsyncSetTo
					(
						Task<T> value
					)
					=> reference.Value = await value;

				public void SetTo
					(
						T value
					)
					=> reference.Value = value;
			}
	}