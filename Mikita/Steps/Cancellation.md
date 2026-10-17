# Cooperative Step Cancellation

Use `Walk.Cancellable(token => steps)` to create a reusable cancellable step. Each `Do` starts one cycle and returns its task; each `Undo` cancels that cycle's token, waits for its execution, and reverses it. Calls alternate `Do`, `Undo`, `Do`, `Undo`, so one step serves repeated cycles. `Undo` without a preceding `Do` does nothing.

The public API is not thread-safe: the owner serializes access and issues calls in order, and does not have to await one call's task before making the next. Each call captures its own cycle's state, so a later `Do` may replace the references while an earlier `Undo` is still completing; the cycles do not interfere. A stronger guarantee, such as never holding two resources at once, belongs to the owning composition.

```csharp
var step = Walk.Cancellable(cancel =>
	[
		Step.NewScoped
			(
				initialize: token => source.Acquire(token),
				release: resource => resource.Release(),
				pattern: resource => Walk.Of(resource.Steps, cancel),
				cancel
			)
	]);

var execution = step.Do();
await step.Undo();
```

`Walk.Of(steps, token)` creates an `AsyncWalk` that borrows an execution token without owning a cancellation source. It checks cancellation before each step and after the final step. Successfully completed steps are recorded before cancellation can trigger their reverse-order rollback. Pass the token explicitly to nested walks and cancellable step overloads. Non-cancellable steps remain usable: they finish before the walk can roll them back.

`Step.That(forward, undo, token)` and `Step.Forward(forward, token)` supply cancellation to forward work. A failing leaf step must clean up its own partial effects. A walk cannot undo a failed leaf whose acquisition state it does not own.

`Step.NewScoped(initialize, release, pattern, token)` owns the initializer's returned value. It releases that value if cancellation wins after acquisition, if pattern construction or execution fails, or after the successful pattern is undone. The pattern must roll back its own partial execution on failure. Acquisition must clean up partial effects if it throws before returning a value. Cleanup never receives the cancelled execution token.

Expected execution cancellation leaves `Do` cancelled only after rollback completes. `Undo` waits for that execution: if it was cancelled, the rollback already happened and `Undo` suppresses the expected cancellation; if it succeeded, `Undo` performs the rollback. `Undo` reports real execution failures, cancellation-callback failures, and cleanup failures. Awaiting `Undo` suffices for cleanup; consumers may also await `Do` to inspect its outcome. A cancelled execution with failed rollback is faulted, not successfully cancelled.

Cancellation does not retract synchronous callbacks already running. Serialize event delivery with subscription removal before disposing resources used by callbacks. A callback may initiate asynchronous teardown, but must not synchronously wait for teardown that needs the callback to return. Where a public value must reset immediately on selection change, a callback checks its own cycle's token under the same serialization boundary as that reset, so it cannot publish an obsolete value. This does not protect reads of disposed resources, which remain the subscription owner's responsibility.