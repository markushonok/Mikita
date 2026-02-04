using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Structs.Enumerables;

public static class ForEachDoing
	{
		extension<T>(IEnumerable<T> enumerable)
			{
				public void ForEach
					(
						Action<T> action
					)
					{
						foreach (var element in enumerable.ToArray())
							action(element);
					}

				public async Task ForEachAsync
					(
						Func<T, Task> action
					)
					{
						foreach (var element in enumerable)
							{
								await action(element)
									.ConfigureAwait(continueOnCapturedContext: false);
							}
					}

				public async Task ForEachAsync
					(
						Func<T, Task> action,
						Func<T, Task> counteraction,
						CancellationToken cancellation = default
					)
					{
						var processed = new Stack<T>();

						try
							{
								foreach (var element in enumerable)
									{
										cancellation.ThrowIfCancellationRequested();
										await action(element)
											.ConfigureAwait(continueOnCapturedContext: false);
										processed.Push(element);
									}
							}
						catch (Exception exception)
							{
								var exceptions = new List<Exception>();

								await processed
									.RollbackEachAsync(counteraction, exceptions)
									.ConfigureAwait(continueOnCapturedContext: false);

								if (exceptions is []) throw;

								exceptions.Insert(0, exception);
								throw new AggregateException(exceptions);
							}
					}
			}

		private static async Task RollbackEachAsync<T>
			(
				this Stack<T> processed,
				Func<T, Task> counteraction,
				ICollection<Exception> exceptions
			)
			{
				while (processed.Count > 0)
					{
						try
							{
								await counteraction(processed.Pop())
									.ConfigureAwait(continueOnCapturedContext: false);
							}
						catch (Exception exception)
							{
								exceptions.Add(exception);
							}
					}
			}
	}