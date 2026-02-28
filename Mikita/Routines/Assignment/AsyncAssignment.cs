using System.Threading.Tasks;

namespace Mikita.Routines.Assignment;

public static class AsyncAssignment
	{
		extension<T>(IAssignment<T> assignment)
			{
				public async Task SetTo(Task<T> value)
					=> assignment.SetTo(await value);
			}
	}