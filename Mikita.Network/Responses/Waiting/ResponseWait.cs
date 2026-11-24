using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;
using Mikita.Threading;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Responses.Waiting;

public sealed class ResponseWait<T>
	(
		IMessageMatchMap<T> matches,
		TaskCompletionSource<Match<T>> outcome
	)
	: IResponseWait<T>
	{
		IAwaiter ITask.GetAwaiter()
			=> GetAwaiter().AsITaskAwaiter;

		public TaskAwaiter GetAwaiter()
			=> ((Task) outcome.Task).GetAwaiter();

		public async Task Match(T outcomes)
			=> (await outcome.Task).Invoke(outcomes);

		public bool Take
			(
				MessageTypeId typeId,
				ReadOnlySpan<byte> bytes
			)
			{
				try
					{
						var match = matches[typeId];
						if (match is null) return false;

						outcome.TrySetResult
							(
								match.Of(bytes)
							);
					}
				catch (Exception exception)
					{
						outcome.TrySetException(exception);
					}

				return true;
			}

		public void Cancel(CancellationToken cancel)
			=> outcome.TrySetCanceled(cancel);

		public void Fail(Exception exception)
			=> outcome.TrySetException(exception);
	}