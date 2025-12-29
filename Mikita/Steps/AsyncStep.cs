using Mikita.Routines;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public sealed class AsyncStep
	(
		CancellableTask @do,
		CancellableTask undo
	)
	: IAsyncStep
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