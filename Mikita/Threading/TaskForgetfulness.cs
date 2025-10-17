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

		[DebuggerStepThrough]
		public static void Forget
			(
				this Task task
			)
			{
				if (task.IsCompletedSuccessfully)
					return;

				task.ContinueWith
					(
						TouchException,
						OnlyOnFaulted | ExecuteSynchronously
					);
			}

		public static void TouchException
			(
				this Task task
			)
			=> _ = task.Exception!;
	}