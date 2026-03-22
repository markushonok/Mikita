using Mikita.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Routines;

public delegate Task CancellableTask
	(
		CancellationToken cancel = default
	);

public delegate Task<T> CancellableTask<T>
	(
		CancellationToken cancel = default
	);