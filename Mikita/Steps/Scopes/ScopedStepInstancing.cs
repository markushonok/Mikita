using Mikita.Routines;
using Mikita.Steps.Walking;
using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps.Scopes;

/// <summary>Provides scoped step compositions.</summary>
/// <remarks>
/// Initializer overloads pass the result to the pattern without a null check.
/// The same T preserves the result's nullability in the pattern parameter.
/// Overloads with an explicit reference require it to hold a non-null value
/// when the pattern is created. Initializing an explicit reference resets it
/// when undone.
/// </remarks>
public static class ScopedStepInstancing
	{
		extension(Step)
			{
				public static IAsyncStep NewScoped<T>
					(
						CancellableTask<T> initialize,
						Func<T, IAsyncStep> pattern
					)
					=> Step.NewScoped<T>
						(
							() => initialize(),
							pattern
						);

				public static IAsyncStep NewScoped<T>
					(
						Func<Task<T>> initialize,
						Func<T, IAsyncStep> pattern
					)
					=> new ScopedStep<T>
						(
							pattern,
							current: Ref<IAsyncStep>.Null,
							value: initialize
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
					=> Step.NewScoped<T>
						(
							() => Task.FromResult(initialize()),
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
							() => Task.FromResult(value.NotNull().Value)
						);
			}
	}