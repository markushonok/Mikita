using Mikita.Routines;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public interface ITaskPool
	{
		void Put
			(
				CancellableTask task
			);

		Task Stop();
	}