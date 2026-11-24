using Mikita.Steps;
using Mikita.Steps.Marching;
using Mikita.Steps.Walking;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Network.Running;

public static class PeerRunInstancing
	{
		extension(PeerRun)
			{
				public static PeerRun With
					(
						IEnumerable<IPeerRun> runs
					)
					=> PeerRun.By(runs.Select(x => x.AsStep));

				public static PeerRun By
					(
						IEnumerable<IAsyncStep> steps
					)
					=> PeerRun.By(Walk.Of(steps));

				public static PeerRun By
					(
						IAsyncStep step
					)
					=> new
						(
							start: step.Do,
							stop: step.Undo
						);

				/// <summary>
				/// Starts the runs concurrently and
				/// stops them concurrently.
				/// </summary>
				/// <remarks>
				/// Use only when the runs are
				/// independent in both directions;
				/// see <see cref="AsyncMarch"/>.
				/// </remarks>
				public static PeerRun Parallel
					(
						IEnumerable<IAsyncStep> steps
					)
					=> PeerRun.By(March.Of(steps));

				public static PeerRun Parallel
					(
						IEnumerable<IPeerRun> runs
					)
					=> PeerRun.Parallel(runs.Select(x => x.AsStep));
			}
	}