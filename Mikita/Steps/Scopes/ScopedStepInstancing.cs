using Mikita.Routines;
using Mikita.Steps.Walking;
using Mikita.Structs.Referring;
using System;

namespace Mikita.Steps.Scopes;

public static class ScopedStepInstancing
	{
		extension(Step)
			{
				public static IAsyncStep NewScoped<T>
					(
						Func<T, IAsyncStep> pattern,
						CancellableTask<T> initialize
					)
					=> Step.NewScoped
						(
							pattern,
							initialize,
							value: Ref<T>.Default
						);

				public static IAsyncStep NewScoped<T>
					(
						Func<T, IAsyncStep> pattern,
						CancellableTask<T> initialize,
						IRef<T?> value
					)
					=> Walk.Of
						([
							value.SetOf(initialize),
							Step.NewScoped(pattern, value)
						]);

				public static IAsyncStep NewScoped<T>
					(
						Func<T, IAsyncStep> pattern,
						Func<T> initialize
					)
					=> Step.NewScoped
						(
							pattern,
							initialize,
							value: Ref<T>.Default
						);

				public static IAsyncStep NewScoped<T>
					(
						Func<T, IAsyncStep> pattern,
						Func<T> initialize,
						IRef<T?> value
					)
					=> Walk.Of
						([
							value.SetOf(initialize).AsAsync,
							Step.NewScoped(pattern, value)
						]);

				public static IAsyncStep NewScoped<T>
					(
						Func<T, IAsyncStep> pattern,
						IRef<T?> value
					)
					=> new ScopedStep<T>
						(
							pattern,
							current: Ref<IAsyncStep>.Null,
							value
						);
			}
	}