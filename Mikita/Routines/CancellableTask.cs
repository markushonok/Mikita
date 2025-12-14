using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Routines;

public delegate Task CancellableTask
	(
		CancellationToken cancellation
	);

public delegate Task<T> CancellableTask<T>
	(
		CancellationToken cancellation
	);