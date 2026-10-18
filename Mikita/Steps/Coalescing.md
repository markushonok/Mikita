# Coalesced Step Calls

`IAsyncStep` requires alternating calls and does not support a repeated or reversed one. `source.WithCoalescing` runs the source as a `CoalescingStep` whose calls declare the desired state instead of scheduling an operation. Use it when an owner must call `Undo` before `Do` finishes, or `Do` before `Undo` finishes, without overlapping the source's operations.

`Do` declares that the source should be applied; `Undo` declares that it should be released. A `Do` following a declared `Do` throws `InvalidOperationException` synchronously, whether the first operation is pending or complete; rejection leaves the cycle unchanged. Repeated `Undo` is allowed and does nothing once the source is released.

The latest declaration decides the next operation. Opposite pending declarations cancel each other out: `Do` in progress, followed by `Undo` and `Do`, executes only the original `Do` when cleanup has not started, and all three calls succeed. If cleanup has already started, it finishes before acquisition starts again.

A call's task completes when no operation is in flight and the source matches the latest declaration. Success means the declaration was accounted for, not that the source passed through its requested state.

```csharp
var step = Step.That
	(
		() => source.Adopt(),
		() => source.Return()
	)
	.AsAsync
	.WithCoalescing;

var applying = step.Do();
var releasing = step.Undo();   // waits for Adopt, then runs Return
await applying;
await releasing;
```

Any failure other than expected cancellation marks the step failed, and the source is never called again. A failure means its state is unknown, so the step stops instead of guessing. The failed phase records only what may remain applied, since that is the one fact a later release needs.

A failed `Do` reports its execution failure to `Do` callers. Waiting and later `Undo` callers succeed without calling the source, because the source cleaned up and nothing is applied. A later `Do` reports the retained execution failure. A failed `Do` that reports `RollbackException`, and any failed `Undo`, report cleanup failure to both directions, because the source may remain applied and the holder must learn that cleanup failed.

Expected cancellation settles the cycle and cancels its callers without failing the step, so a later acquire may run. The source must roll back its partial effects when `Do` fails or is cancelled, as `Walk` does, and report failed rollback through `RollbackException`.

There are no retries after a failure. An owner that must recover creates a new step, as a slot does when it replaces a load.

The ledger is supplied through the primary constructor: a `StepState` phase, the retained failure, separate pending completions for `Do` and `Undo`, and the access that serializes them. The phase carries the latest declaration, whether the source reflects it, and whether the cycle failed. `WithCoalescing` creates a cycle in `Undone` with no retained failure or pending completions. The injected state must belong exclusively to this cycle and must not be changed outside its access boundary. Calls may arrive concurrently; checking that the cycle has settled and completing its pending calls occur under one lock, so a new declaration either belongs to that cycle or starts a new one. Source operations run outside the lock. Never synchronously wait for the cycle from an operation it runs.