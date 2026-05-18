using System.Collections.Generic;

namespace Mikita.Steps.Walking;

public static class WalkInstancing
	{
		extension(Walk)
			{
				public static IAsyncStep Of
					(
						IEnumerable<IAsyncStep> steps
					)
					=> new AsyncWalk(steps);

				public static IStep Of
					(
						IEnumerable<IStep> steps
					)
					=> new Walk(steps);
			}
	}