using Mikita.Structs.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Mikita.Steps.Walking;

/// <summary>
/// Performs its steps in order and undoes them in reverse order.
/// </summary>
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