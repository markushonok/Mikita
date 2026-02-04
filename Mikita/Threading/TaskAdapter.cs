using System.Threading.Tasks;

namespace Mikita.Threading;

public sealed class TaskAdapter
	(
		Task task
	)
	: ITask
	{
		public IAwaiter GetAwaiter()
			=> task.GetAwaiter().AsITaskAwaiter;
	}

public sealed class TaskAdapter<T>
	(
		Task<T> task
	)
	: ITask<T>
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter();

		public IAwaiter<T> GetAwaiter()
			=> task.GetAwaiter().AsITaskAwaiter;
	}