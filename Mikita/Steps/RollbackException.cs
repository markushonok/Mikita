using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Mikita.Steps;

/// <summary>
/// Reports failed execution together with failures of its rollback. A failed
/// leaf that cannot finish its own cleanup must report both through this type.
/// An ordinary AggregateException does not imply a rollback failure.
/// </summary>
public sealed class RollbackException
	(
		Exception executionFailure,
		ImmutableArray<Exception> rollbackFailures
	)
	: AggregateException([executionFailure, .. rollbackFailures])
	{
		/// <summary>The original forward failure, excluding rollback.</summary>
		public Exception ExecutionFailure
			=> executionFailure is RollbackException nested
				? nested.ExecutionFailure
				: executionFailure;

		/// <summary>
		/// Cleanup failures, including rollback of a failed nested composition.
		/// </summary>
		public IReadOnlyList<Exception> RollbackFailures
			=> executionFailure is RollbackException nested
				? [.. nested.RollbackFailures, .. rollbackFailures]
				: rollbackFailures;
	}