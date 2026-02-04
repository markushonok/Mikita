using System.Diagnostics;
using System.Threading.Tasks;
using static System.Threading.Tasks.TaskContinuationOptions;

namespace Mikita.Threading;

public static class TaskForgetfulness
	{
		[DebuggerStepThrough]
		public static void Forget
			(
				this ValueTask task
			)
			{
				if (task.IsCompletedSuccessfully)
					return;

				task.AsTask().Forget();
			}

		[DebuggerStepThrough]
		public static void Forget<T>
			(
				this ValueTask<T> task
			)
			{
				if (task.IsCompletedSuccessfully)
					return;

				task.AsTask().Forget();
			}

		extension(Task task)
			{
				[DebuggerStepThrough]
				public void Forget()
					{
						if (task.IsCompletedSuccessfully)
							return;

						task.ContinueWith
							(
								TouchException,
								OnlyOnFaulted | ExecuteSynchronously
							);
					}

				public void TouchException()
					=> _ = task.Exception!;
			}

	}