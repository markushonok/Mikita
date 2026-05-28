using System;
using System.Threading.Tasks;

namespace Mikita.Threading.Pools;

public static class TaskPutting
	{
		extension(ITaskPool pool)
			{
				public void Put(Func<Task> task)
					=> pool.Put(cancel => task());
			}
	}