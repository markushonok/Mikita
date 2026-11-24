using Mikita.Threading;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Requests;

public static class CancellationMatchTask
	{
		public static IMatchTask<T> From<T>
			(
				IMatchTask<T> source,
				CancellationToken cancel,
				Action cancellation
			)
			{
				if (!cancel.CanBeCanceled)
					return source;

				var registration = cancel.Register(cancellation);
				return new CancellationMatchTask<T>
					(
						source,
						Await(source, registration)
					);
			}

		private static async Task Await<T>
			(
				IMatchTask<T> source,
				CancellationTokenRegistration registration
			)
			{
				await using (registration)
					await source;
			}
	}

public sealed class CancellationMatchTask<T>
	(
		IMatchTask<T> source,
		Task completion
	)
	: IMatchTask<T>
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter().AsITaskAwaiter;

		public TaskAwaiter GetAwaiter()
			=> completion.GetAwaiter();

		public async Task Match(T outcomes)
			{
				await completion;
				await source.Match(outcomes);
			}
	}