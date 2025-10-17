using Godot;

namespace Mikita.Godot.Threading;

public static class Deferred
	{
		public static Task<T> ResultOf<T>
			(
				Func<T> action
			)
			{
				var completion = new TaskCompletionSource<T>(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(() =>
						{
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
				Func<Task<T>> action
			)
			{
				var completion = new TaskCompletionSource<T>(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(async () =>
						{
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
				Action action
			)
			{
				var completion = new TaskCompletionSource(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(() =>
						{
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
				Func<Task> action
			)
			{
				var completion = new TaskCompletionSource(
					TaskCreationOptions.RunContinuationsAsynchronously);

				Callable
					.From(async () =>
						{
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