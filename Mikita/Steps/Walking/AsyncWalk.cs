using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikita.Steps.Walking;

/// <summary>
/// Performs its steps in order and undoes them in reverse order.
/// </summary>
/// <remarks>
/// A failed <see cref="Do"/> undoes the steps it has already performed, and a
/// failed <see cref="Undo"/> re-performs the steps it has already undone.
/// </remarks>
public sealed class AsyncWalk
	(
		IEnumerable<IAsyncStep> steps
	)
	: IAsyncStep
	{
		public Task Do()
			=> steps.ForEachAsync
				(
					action: x => x.Do(),
					counteraction: x => x.Undo()
				);

		public Task Undo()
			=> steps
				.Reverse()
				.ForEachAsync
					(
						action: x => x.Undo(),
						counteraction: x => x.Do()
					);
	}