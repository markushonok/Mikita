using TDeferred = Mikita.Godot.Threading.Deferred;

namespace Mikita.Godot.Threading;

public static class DeferredOn
	{
		public static Task<T> Deferred<TTarget, T>
			(
				this TTarget target,
				Func<TTarget, T> function
			)
			=> TDeferred.ResultOf(() => function(target));

		public static Task<T> Deferred<TTarget, T>
			(
				this TTarget target,
				Func<TTarget, Task<T>> function
			)
			=> TDeferred.ResultOf(() => function(target));

		public static Task Defer<T>
			(
				this T target,
				Action<T> action
			)
			=> TDeferred.Do(() => action(target));
	}