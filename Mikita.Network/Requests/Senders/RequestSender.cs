using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Packets.Senders;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests.Contracts;
using Mikita.Network.Requests.Registries;
using Mikita.Network.Responses.Waiting;
using Mikita.Structs.Buffers.Writing;
using Mikita.Threading;
using System;
using System.Threading;

namespace Mikita.Network.Requests.Senders;

public sealed class RequestSender
	(
		IMessageRegistry messages,
		IPacketSender<PeerKey> sender,
		IRequestRegistry requests,
		IByteWriterPool writers,
		IResponseWaitMap waits
	)
	: IRequestSender
	{
		public IMatchTask<THook> Send<TMessage, THook>
			(
				PeerKey recipient,
				TMessage message,
				WriterPutWay<RentedByteWriter, TMessage> serialize,
				IRequestContract<THook> contract,
				CancellationToken cancel = default
			)
			{
				cancel.ThrowIfRequested();

				var request = requests.Register(recipient);
				var wait = Wait(request, contract);
				var operation = CancellationMatchTask.From
					(
						wait,
						cancel,
						() => Cancel(request, cancel)
					);

				try
					{
						Send
							(
								recipient,
								request,
								contract.TypeId,
								message,
								serialize
							);
					}
				catch (Exception exception)
					{
						Fail(request, exception);
					}

				return operation;
			}

		private IMatchTask<T> Wait<T>
			(
				RequestId request,
				IMessageMatchMap<T> responses
			)
			{
				try
					{
						return waits.Wait(request, responses);
					}
				catch
					{
						requests.Release(request);
						throw;
					}
			}

		private void Cancel
			(
				RequestId request,
				CancellationToken cancel
			)
			{
				try
					{
						waits.Cancel(request, cancel);
					}
				finally
					{
						requests.Release(request);
					}
			}

		private void Fail
			(
				RequestId request,
				Exception exception
			)
			{
				try
					{
						waits.Fail(request, exception);
					}
				finally
					{
						requests.Release(request);
					}
			}

		private void Send<TMessage>
			(
				PeerKey recipient,
				RequestId request,
				MessageTypeId typeId,
				TMessage message,
				WriterPutWay<RentedByteWriter, TMessage> serialize
			)
			{
				using var writer = writers.Rent();
				messages.Require(typeId, MessageRole.Request);
				writer.Put(typeId.Value);
				writer.Put(request);
				writer.Put(serialize, message);
				sender.Send(recipient, writer.Written);
			}
	}