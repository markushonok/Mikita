using Mikita.Routines;
using Mikita.Threading;
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
				CancellationToken cancel
			)
			=> @do(cancel);

		public Task Undo
			(
				CancellationToken cancel
			)
			=> undo(cancel);
	}