using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mikita.Steps;

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