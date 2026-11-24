using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests.Contracts;
using Mikita.Structs.Buffers.Writing;
using Mikita.Threading;
using System;
using System.Threading;

namespace Mikita.Network.Requests.Senders;

public static class RequestTimeout
	{
		extension(IRequestSender sender)
			{
				public IMatchTask<THook> SendOrFail<TMessage, THook>
					(
						PeerKey recipient,
						TMessage message,
						WriterPutWay<RentedByteWriter, TMessage> serialize,
						IRequestContract<THook> contract,
						TimeSpan timeout,
						CancellationToken cancel = default
					)
					where THook: IRequestHook
					=> Send
						(
							sender,
							recipient,
							message,
							serialize,
							contract,
							timeout,
							static (hook, duration) => hook.FailWith
								(
									DeadlineTask.TimeoutException(duration).Message
								),
							cancel
						);

				public IMatchTask<THook> SendOrTimeout<TMessage, THook>
					(
						PeerKey recipient,
						TMessage message,
						WriterPutWay<RentedByteWriter, TMessage> serialize,
						IRequestContract<THook> contract,
						TimeSpan timeout,
						CancellationToken cancel = default
					)
					where THook: ITimeoutHook
					=> Send
						(
							sender,
							recipient,
							message,
							serialize,
							contract,
							timeout,
							static (hook, duration) => hook.Timeout(duration),
							cancel
						);
			}

		private static IMatchTask<THook> Send<TMessage, THook>
			(
				IRequestSender sender,
				PeerKey recipient,
				TMessage message,
				WriterPutWay<RentedByteWriter, TMessage> serialize,
				IRequestContract<THook> contract,
				TimeSpan timeout,
				Action<THook, TimeSpan> expiration,
				CancellationToken cancel
			)
			=> DeadlineMatchTask.From
				(
					subCancel => sender.Send
						(
							recipient,
							message,
							serialize,
							contract,
							subCancel
						),
					timeout,
					expiration,
					cancel
				);
	}