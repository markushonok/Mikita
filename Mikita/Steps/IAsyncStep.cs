using Mikita.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public interface IAsyncStep
	{
		Task Do
			(
				CancellationToken cancel = default
			);

		Task Undo
			(
				CancellationToken cancel = default
			);
	}