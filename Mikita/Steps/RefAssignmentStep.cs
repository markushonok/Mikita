using Mikita.Routines;
using Mikita.Structs.Referring;
using System;
using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// Provides steps that assign a reference and reset it when undone.
/// </summary>
/// <remarks>
/// Undo resets the reference to a provided value, or to its default when none
/// is given.
/// </remarks>
public static class RefAssignmentStep
	{
		public static IAsyncStep SetOf<T>
			(
				this IRef<T> reference,
				CancellableTask<T> initialize,
				T deinitialize
			)
			=> Step.That
				(
					@do: async () => reference.SetTo(await initialize()),
					undo: async () => reference.SetTo(deinitialize)
				);

		extension<T>(IRef<T?> reference)
			{
				public IAsyncStep SetOf
					(
						CancellableTask<T> initialize
					)
					=> reference.SetOf(() => initialize());

				public IAsyncStep SetOf
					(
						Func<Task<T>> initialize
					)
					=> Step.That
						(
							@do: async () => reference.SetTo(await initialize()),
							undo: async () => reference.SetTo(default)
						);

				public IStep SetOf
					(
						Func<T> initialize
					)
					=> Step.That
						(
							@do: () => reference.SetTo(initialize()),
							undo: () => reference.SetTo(default)
						);

				public IStep SetOf
					(
						Func<T> initialize,
						T deinitialize
					)
					=> Step.That
						(
							@do: () => reference.SetTo(initialize()),
							undo: () => reference.SetTo(deinitialize)
						);
			}
	}