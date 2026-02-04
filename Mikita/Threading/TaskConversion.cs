using System.Threading.Tasks;

namespace Mikita.Threading;

public static class TaskConversion
	{
		extension<T>(Task<T> task)
			{
				public TaskAdapter<T> AsITask
					=> new(task);
			}

		extension(Task task)
			{
				public TaskAdapter AsITask
					=> new(task);
			}
	}