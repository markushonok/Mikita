using TDeferred = Mikita.Godot.Threading.Deferred;

namespace Mikita.Godot.Threading;

public static class DeferredOn
	{
		public static Task<T> Deferred<TTarget, T>
			(
				this TTarget target,
				Func<TTarget, T> function,
				CancellationToken cancellation = default
			)
			=> TDeferred.ResultOf
				(
					() => function(target),
					cancellation
				);

		public static Task<T> Deferred<TTarget, T>
			(
				this TTarget target,
				Func<TTarget, Task<T>> function,
				CancellationToken cancellation = default
			)
			=> TDeferred.ResultOf
				(
					() => function(target),
					cancellation
				);

		public static Task Defer<T>
			(
				this T target,
				Action<T> action,
				CancellationToken cancellation = default
			)
			=> TDeferred.Do
				(
					() => action(target),
					cancellation
				);
	}