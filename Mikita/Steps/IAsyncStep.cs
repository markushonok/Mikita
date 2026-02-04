using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public interface IAsyncStep
	{
		Task Do
			(
				CancellationToken cancellation = default
			);

		Task Undo
			(
				CancellationToken cancellation = default
			);
	}