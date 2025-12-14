using System;

namespace Mikita.Steps;

partial class Step
	{
		public static Step Forward(Action @do)
			=> new
				(
					@do,
					undo: delegate {}
				);
	}