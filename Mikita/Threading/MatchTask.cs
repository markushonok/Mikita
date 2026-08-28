using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mikita.Threading;

/// <summary>
/// Represents a reusable match task backed by an already-created operation.
/// </summary>
/// <param name="outcome">
/// The hot operation that produces the semantic match.
/// </param>
public sealed class MatchTask<T>
	(
		Task<Match<T>> outcome
	)
	: IMatchTask<T>
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter().AsITaskAwaiter;

		public TaskAwaiter GetAwaiter()
			=> ((Task)outcome).GetAwaiter();

		public async Task Match(T outcomes)
			=> (await outcome).Invoke(outcomes);
	}

public static class MatchTask
	{
		public static IMatchTask<T> From<T>
			(
				Func<Task<Match<T>>> outcome
			)
			=> From(outcome());

		public static IMatchTask<T> From<T>
			(
				Match<T> outcome
			)
			=> MatchTask.From(Task.FromResult(outcome));

		public static IMatchTask<T> From<T>
			(
				Task<Match<T>> outcome
			)
			=> new MatchTask<T>(outcome);
	}