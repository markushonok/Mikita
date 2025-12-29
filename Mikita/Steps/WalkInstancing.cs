using System.Collections.Generic;

namespace Mikita.Steps;

public static class WalkInstancing
	{
		extension(Walk)
			{
				public static Walk Of
					(
						IEnumerable<IStep> steps
					)
					=> new(steps);

				public static AsyncWalk Of
					(
						IEnumerable<IAsyncStep> steps
					)
					=> new(steps);
			}
	}