using System;

namespace Mikita.Steps;

public sealed partial class Step
	(
		Action @do,
		Action undo
	)
	: IStep
	{
		public void Do()
			=> @do();

		public void Undo()
			=> undo();
	}