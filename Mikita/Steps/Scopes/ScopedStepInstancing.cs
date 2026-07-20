using Mikita.Routines;
using Mikita.Steps.Walking;
using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Scopes;

public static class ScopedStepInstancing
	{
		extension(Step)
			{
				public static IAsyncStep NewScoped<T>
					(
						CancellableTask<T> initialize,
						Func<T, IAsyncStep> pattern
					)
					=> Step.NewScoped
						(
							() => initialize(),
							pattern
						);

				public static IAsyncStep NewScoped<T>
					(
						Func<Task<T>> initialize,
						Func<T, IAsyncStep> pattern
					)
					=> Step.NewScoped
						(
							initialize,
							value: Ref<T>.Default,
							pattern
						);

				public static IAsyncStep NewScoped<T>
					(
						Func<Task<T>> initialize,
						IRef<T?> value,
						Func<T, IAsyncStep> pattern
					)
					=> Walk.Of
						([
							value.SetOf(initialize),
							Step.NewScoped(value, pattern)
						]);

				public static IAsyncStep NewScoped<T>
					(
						Func<T> initialize,
						Func<T, IAsyncStep> pattern
					)
					=> Step.NewScoped
						(
							initialize,
							value: Ref<T>.Default,
							pattern
						);

				public static IAsyncStep NewScoped<T>
					(
						Func<T> initialize,
						IRef<T?> value,
						Func<T, IAsyncStep> pattern
					)
					=> Walk.Of
						([
							value.SetOf(initialize).AsAsync,
							Step.NewScoped(value, pattern)
						]);

				public static IAsyncStep NewScoped<T>
					(
						IRef<T?> value,
						Func<T, IAsyncStep> pattern
					)
					=> new ScopedStep<T>
						(
							pattern,
							current: Ref<IAsyncStep>.Null,
							value.NotNull().AsFunction
						);
			}
	}