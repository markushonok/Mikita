using Mikita.Structs.Maps;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mikita.Threading;

public static class SafeMatchTask
	{
		extension<T>(IMatchTask<T> source)
			where T: IFailureHook
			{
				public IMatchTask<T> With
					(
						IReadOnlyMap<Exception, string> messages
					)
					=> new SafeMatchTask<T>(source, messages);

				public IMatchTask<T> With(string message)
					=> source.With
						(
							Map.Of<Exception, string>(_ => message)
						);
			}
	}

public sealed class SafeMatchTask<T>
	(
		IMatchTask<T> source,
		IReadOnlyMap<Exception, string> messages
	)
	: IMatchTask<T>
	where T: IFailureHook
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter().AsITaskAwaiter;

		public TaskAwaiter GetAwaiter()
			=> source.GetAwaiter();

		public async Task Match(T outcomes)
			{
				try
					{
						await source;
					}
				catch (Exception exception)
					when (exception is not OperationCanceledException)
					{
						outcomes.FailWith(messages[exception]);
						return;
					}

				await source.Match(outcomes);
			}
	}
