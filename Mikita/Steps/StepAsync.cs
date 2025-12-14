using Mikita.Routines;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public sealed class StepAsync
	(
		CancellableTask @do,
		CancellableTask undo
	)
	: IStepAsync
	{
		public Task Do
			(
				CancellationToken cancellation
			)
			=> @do(cancellation);

		public Task Undo
			(
				CancellationToken cancellation
			)
			=> undo(cancellation);
	}