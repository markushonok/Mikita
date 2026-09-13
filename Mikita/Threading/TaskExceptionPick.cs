using System;
using System.Threading.Tasks;

namespace Mikita.Threading;

/// <summary>
/// Converts unsuccessful task completion into an exception value.
/// </summary>
public static class TaskExceptionPick
	{
		extension(Task task)
			{
				/// <summary>
				/// Waits for the task and returns its thrown exception, including
				/// cancellation; returns <see langword="null"/> on success.
				/// </summary>
				public Task<Exception?> WaitException
					=> WaitExceptionFrom(task);
			}

		private static async Task<Exception?> WaitExceptionFrom(Task task)
			{
				try
					{
						await task;
						return null;
					}
				catch (Exception exception)
					{
						return exception;
					}
			}
	}