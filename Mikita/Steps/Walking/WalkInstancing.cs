using System;
using System.Collections.Generic;
using System.Threading;

namespace Mikita.Steps.Walking;

/// <summary>
/// Provides standard walk compositions.
/// </summary>
public static class WalkInstancing
	{
		extension(Walk)
			{
				/// <summary>
				/// Creates a reusable cancellable step over sequential steps. Undo
				/// during Do cancels execution and awaits rollback; pass the token
				/// to nested walks and cancellable operations.
				/// </summary>
				public static IAsyncStep Of
					(
						Func<CancellationToken, IEnumerable<IAsyncStep>> steps
					)
					=> Step.With
						(
							cancel => Walk.Of(steps(cancel), cancel)
						);

				/// <summary>
				/// Checks cancellation between steps and after the last step;
				/// completed steps roll back without the execution token.
				/// </summary>
				public static IAsyncStep Of
					(
						IEnumerable<IAsyncStep> steps,
						CancellationToken cancel
					)
					=> new AsyncWalk(steps, cancel);

				public static IAsyncStep Of
					(
						IEnumerable<IAsyncStep> steps
					)
					=> new AsyncWalk(steps);

				public static IStep Of
					(
						IEnumerable<IStep> steps
					)
					=> new Walk(steps);
			}
	}