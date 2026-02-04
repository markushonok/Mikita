using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Steps;

public sealed class Walk
	(
		IEnumerable<IStep> steps
	)
	: IStep
	{
		public void Do()
			=> steps.ForEach(x => x.Do());

		public void Undo()
			=> steps
				.Reverse()
				.ForEach(x => x.Undo());
	}