using Mikita.Network.Peers.Keys;
using Mikita.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Messages.Receipt;

public static class MessageReceiveWait
	{
		public static Task<T> Until<T>
			(
				this IMessageEventMap<T> source,
				PeerKey senderKey,
				Predicate<T> filter,
				TimeSpan duration,
				CancellationToken cancel = default
			)
			=> DeadlineTask.From
				(
					subCancel => source.Until(senderKey, filter, subCancel),
					duration,
					cancel
				);

		public static Task<T> Until<T>
			(
				this IMessageEventMap<T> source,
				PeerKey senderKey,
				Predicate<T> filter,
				CancellationToken cancel = default
			)
			=> source
				.With(senderKey)
				.Until(filter, cancel);

		public static async Task<T> Until<T>
			(
				this IMessageEvent<T> source,
				Predicate<T> filter,
				CancellationToken cancel = default
			)
			{
				var taskCompletion = new TaskCompletionSource<T>();

				void Reaction(T message)
					{
						if (filter(message))
							taskCompletion.TrySetResult(message);
					}

				source.Add(Reaction);

				try
					{
						await using var registration = cancel.Register
							(
								() => taskCompletion.TrySetCanceled(cancel)
							);
						return await taskCompletion.Task;
					}
				finally
					{
						source.Remove(Reaction);
					}
			}
	}