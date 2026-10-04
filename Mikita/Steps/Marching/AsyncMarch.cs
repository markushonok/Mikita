using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Mikita.Steps.Marching;

/// <summary>
/// Performs its steps concurrently and undoes them concurrently.
/// </summary>
/// <remarks>
/// The steps must be independent: each step must tolerate running at the same
/// time as the others, in both the forward and the reverse direction. A failed
/// <see cref="Do"/> undoes the steps that performed successfully, and a failed
/// <see cref="Undo"/> re-performs the steps that reversed successfully. Every
/// started operation is awaited before compensation, so no step is reversed
/// while its own operation is still running. Failures from the operations and
/// from their compensation are reported together.
/// </remarks>
public sealed class AsyncMarch
	(
		IEnumerable<IAsyncStep> steps
	)
	: IAsyncStep
	{
		public Task Do()
			=> Perform
				(
					forward: x => x.Do(),
					backward: x => x.Undo()
				);

		public Task Undo()
			=> Perform
				(
					forward: x => x.Undo(),
					backward: x => x.Do()
				);

		private async Task Perform
			(
				Func<IAsyncStep, Task> forward,
				Func<IAsyncStep, Task> backward
			)
			{
				var ordered = steps.ToArray();
				var operations = ordered
					.Select(x => Start(forward, x))
					.ToArray();
				var failures = await AwaitAll(operations);

				if (failures.Count is 0) return;

				var performed = ordered
					.Where
						(
							(_, index) => operations[index]
								.IsCompletedSuccessfully
						)
					.Select(x => Start(backward, x))
					.ToArray();
				var undoFailures = await AwaitAll(performed);

				Throw(failures, undoFailures);
			}

		private static Task Start
			(
				Func<IAsyncStep, Task> operation,
				IAsyncStep step
			)
			{
				try
					{
						return operation(step) ?? Task.CompletedTask;
					}
				catch (Exception exception)
					{
						return Task.FromException(exception);
					}
			}

		private static async Task<IReadOnlyList<Exception>> AwaitAll
			(
				IReadOnlyList<Task> operations
			)
			{
				var failures = new List<Exception>();

				foreach (var operation in operations)
					{
						try
							{
								await operation;
							}
						catch (Exception exception)
							{
								failures.Add(exception);
							}
					}

				return failures;
			}

		private static void Throw
			(
				IReadOnlyList<Exception> failures,
				IReadOnlyList<Exception> undoFailures
			)
			{
				if (undoFailures.Count is 0 && failures.Count is 1)
					ExceptionDispatchInfo
						.Capture(failures[0])
						.Throw();

				throw new AggregateException
					(
						failures.Concat(undoFailures)
					);
			}
	}