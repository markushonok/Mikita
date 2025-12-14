using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public sealed class WalkAsync
	(
		IEnumerable<IStepAsync> steps
	)
	: IStepAsync
	{
		public Task Do
			(
				CancellationToken cancellation
			)
			=> steps.ForEachAsync
				(
					action: x => x.Do(cancellation),
					counteraction: x => x.Undo(cancellation),
					cancellation
				);

		public Task Undo
			(
				CancellationToken cancellation
			)
			=> steps
				.Reverse()
				.ForEachAsync
					(
						action: x => x.Undo(cancellation),
						counteraction: x => x.Do(cancellation),
						cancellation
					);
	}