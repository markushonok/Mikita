using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mikita.Threading;

/// <summary>
/// Represents a reusable match task backed by an already-created operation.
/// </summary>
/// <param name="outcome">
/// The hot operation that produces the semantic outcome dispatcher.
/// </param>
public sealed class MatchTask<T>
	(
		Task<Action<T>> outcome
	)
	: IMatchTask<T>
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter().AsITaskAwaiter;

		public TaskAwaiter GetAwaiter()
			=> ((Task)outcome).GetAwaiter();

		public async Task Match(T outcomes)
			=> (await outcome)(outcomes);
	}

public static class MatchTask
	{
		public static IMatchTask<T> From<T>
			(
				Func<Task<Action<T>>> outcome
			)
			=> From(outcome());

		public static IMatchTask<T> From<T>
			(
				Action<T> outcome
			)
			=> MatchTask.From(Task.FromResult(outcome));

		public static IMatchTask<T> From<T>
			(
				Task<Action<T>> outcome
			)
			=> new MatchTask<T>(outcome);
	}