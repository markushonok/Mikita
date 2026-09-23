using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Godot.Threading;

public static class Deferred
	{
		public static Task<T> ResultOf<T>
			(
				Func<T> action,
				CancellationToken cancellation = default
			)
			{
				var completion = new TaskCompletionSource<T>(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(() =>
						{
							if (cancellation.IsCancellationRequested)
								{
									completion.SetCanceled(cancellation);
									return;
								}

							try
								{
									completion.SetResult(action());
								}
							catch (Exception exception)
								{
									completion.SetException(exception);
								}
						})
					.CallDeferred();

				return completion.Task;
			}

		public static Task<T> ResultOf<T>
			(
				Func<Task<T>> action,
				CancellationToken cancellation = default
			)
			{
				var completion = new TaskCompletionSource<T>(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(async void () =>
						{
							if (cancellation.IsCancellationRequested)
								{
									completion.SetCanceled(cancellation);
									return;
								}

							try
								{
									var value = await action().ConfigureAwait(false);
									completion.SetResult(value);
								}
							catch (Exception exception)
								{
									completion.SetException(exception);
								}
						})
					.CallDeferred();

				return completion.Task;
			}

		public static Task Do
			(
				Action action,
				CancellationToken cancellation = default
			)
			{
				var completion = new TaskCompletionSource(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(() =>
						{
							if (cancellation.IsCancellationRequested)
								{
									completion.SetCanceled(cancellation);
									return;
								}

							try
								{
									action();
									completion.SetResult();
								}
							catch (Exception exception)
								{
									completion.SetException(exception);
								}
						})
					.CallDeferred();

				return completion.Task;
			}

		public static Task Do
			(
				Func<Task> action,
				CancellationToken cancellation = default
			)
			{
				var completion = new TaskCompletionSource(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(async void () =>
						{
							if (cancellation.IsCancellationRequested)
								{
									completion.SetCanceled(cancellation);
									return;
								}

							try
								{
									await action().ConfigureAwait(false);
									completion.SetResult();
								}
							catch (Exception exception)
								{
									completion.SetException(exception);
								}
						})
					.CallDeferred();

				return completion.Task;
			}
	}