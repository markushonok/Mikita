using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Pipeline;
using Mikita.Network.Requests;
using Mikita.Network.Requests.Registries;
using Mikita.Structs.Buffers.Reading;
using System;

namespace Mikita.Network.Responses.Waiting;

/// <summary>
/// Routes a response to the request that waits for it. A
/// request sent through a session is matched through the
/// session's current transport, so a response received on that
/// transport completes the request even though the incoming
/// packet carries the transport's own key.
/// </summary>
public sealed class ResponseRouter
	(
		IMessageRegistry messages,
		IRequestRegistry requests,
		IResponseWaitMap responses,
		IPeerAliasMap aliases,
		IMiddleware invalidSize,
		IMiddleware invalidRecipient,
		IMiddleware waitMiss,
		IMiddleware typeMiss
	)
	: IMiddleware
	{
		public void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			)
			{
				if (bytes.Length < HeaderSize)
					{
						invalidSize.Handle(peer, bytes);
						return;
					}

				var reader = new ByteReader(bytes);
				var typeId = new MessageTypeId(reader.GetShort());

				if (messages.LabelOf(typeId)?.Role != MessageRole.Response)
					{
						typeMiss.Handle(peer, bytes);
						return;
					}

				var request = reader.GetRequestId();
				var recipient = requests.RecipientKeyOf(request);
				var expected = recipient is PeerKey key
					? aliases.ReferentOf(key) ?? key
					: default(PeerKey?);

				if (expected != peer)
					{
						invalidRecipient.Handle(peer, bytes);
						return;
					}

				var take = responses.Take
					(
						request,
						typeId,
						bytes[HeaderSize..]
					);

				switch (take)
					{
						case ResponseTake.Complete: requests.Release(request); break;
						case ResponseTake.WaitMiss: waitMiss.Handle(peer, bytes); break;
						case ResponseTake.TypeMiss: typeMiss.Handle(peer, bytes); break;
						default: throw new NotSupportedException();
					}
			}

		private static int HeaderSize
			=> sizeof(short) + sizeof(int);
	}