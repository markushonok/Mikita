using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public sealed class AsyncWalk
	(
		IEnumerable<IAsyncStep> steps
	)
	: IAsyncStep
	{
		public Task Do
			(
				CancellationToken cancel
			)
			=> steps.ForEachAsync
				(
					action: x => x.Do(cancel),
					counteraction: x => x.Undo(cancel),
					cancel
				);

		public Task Undo
			(
				CancellationToken cancel
			)
			=> steps
				.Reverse()
				.ForEachAsync
					(
						action: x => x.Undo(cancel),
						counteraction: x => x.Do(cancel),
						cancel
					);
	}