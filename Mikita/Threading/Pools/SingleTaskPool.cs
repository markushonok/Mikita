using Mikita.Routines;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public sealed class SingleTaskPool
	(
		ITaskPool basePool
	)
	: ITaskPool
	{
		public void Put(CancellableTask task)
			{
				basePool.Stop().Forget();
				basePool.Put(task);
			}

		public Task Stop()
			=> basePool.Stop();
	}