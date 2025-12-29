using TDeferred = Mikita.Godot.Threading.Deferred;

namespace Mikita.Godot.Threading;

public static class DeferredOn
	{
		extension<TTarget>(TTarget target)
			{
				public Task<T> Deferred<T>
					(
						Func<TTarget, T> function,
						CancellationToken cancellation = default
					)
					=> TDeferred.ResultOf
						(
							() => function(target),
							cancellation
						);

				public Task<T> Deferred<T>
					(
						Func<TTarget, Task<T>> function,
						CancellationToken cancellation = default
					)
					=> TDeferred.ResultOf
						(
							() => function(target),
							cancellation
						);

				public Task Defer
					(
						Action<TTarget> action,
						CancellationToken cancellation = default
					)
					=> TDeferred.Do
						(
							() => action(target),
							cancellation
						);
			}

	}