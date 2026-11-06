using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Mikita.Threading;

/// <summary>
/// Reports operation faults through the matched error hook instead of failing
/// before the hook can be invoked.
/// </summary>
public static class ErrorMatchTask
	{
		extension<T>(IMatchTask<T> source)
			where T: IErrorHook
			{
				public IMatchTask<T> WithExceptionMatch
					=> new ErrorMatchTask<T>(source);
			}
	}

public sealed class ErrorMatchTask<T>
	(
		IMatchTask<T> source
	)
	: IMatchTask<T>
	where T: IErrorHook
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
					{
						var captured = ExceptionDispatchInfo.Capture(exception);
						outcomes.Error(captured.Throw);
						return;
					}

				await source.Match(outcomes);
			}
	}