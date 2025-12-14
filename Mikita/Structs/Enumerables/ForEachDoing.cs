using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Structs.Enumerables;

public static class ForEachDoing
	{
		public static void ForEach<T>
			(
				this IEnumerable<T> enumerable,
				Action<T> action
			)
			{
				foreach (var element in enumerable)
					action(element);
			}

		public static async Task ForEachAsync<T>
			(
				this IEnumerable<T> enumerable,
				Func<T, Task> action
			)
			{
				foreach (var element in enumerable)
					{
						await action(element)
							.ConfigureAwait(continueOnCapturedContext: false);
					}
			}

		public static async Task ForEachAsync<T>
			(
				this IEnumerable<T> enumerable,
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