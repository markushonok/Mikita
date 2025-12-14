using System.Collections.Generic;

namespace Mikita.Steps;

partial class Walk
	{
		public static Walk Of
			(
				IEnumerable<IStep> steps
			)
			=> new(steps);
	}