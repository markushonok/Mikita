using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Steps;

public interface IStepAsync
	{
		Task Do
			(
				CancellationToken cancellation
			);

		Task Undo
			(
				CancellationToken cancellation
			);
	}