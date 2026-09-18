using System;
using System.Threading.Tasks;

namespace Mikita.Threading;

/// <summary>
/// Provides task-returning invocation with thrown exceptions represented by
/// returned tasks.
/// </summary>
/// <remarks>
/// An <see cref="OperationCanceledException"/> thrown before an operation
/// returns its task is represented by a faulted task, not a cancelled task.
/// </remarks>
public static class TaskThrowHandling
	{
		extension<TTarget>(TTarget target)
			{
				/// <summary>
				/// Performs the operation and returns its task, or a faulted task
				/// when the operation throws before returning one.
				/// </summary>
				public Task DoWithThrowAsTask
					(
						Func<TTarget, Task> operation
					)
					{
						try
							{
								return operation(target);
							}
						catch (Exception exception)
							{
								return Task.FromException(exception);
							}
					}

				/// <summary>
				/// Performs the operation and returns its task, or a faulted task
				/// when the operation throws before returning one.
				/// </summary>
				public Task<TResult> DoWithThrowAsTask<TResult>
					(
						Func<TTarget, Task<TResult>> operation
					)
					{
						try
							{
								return operation(target);
							}
						catch (Exception exception)
							{
								return Task.FromException<TResult>(exception);
							}
					}
			}
	}