using Mikita.Network.Messages;
using Mikita.Network.Peers.Keys;
using System;

namespace Mikita.Network.Pipeline;

public sealed class MessageMiddleware
	(
		IMiddleware middleware,
		MessageTypeId typeId
	)
	: IMessageMiddleware
	{
		public void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			)
			=> middleware.Handle(peer, bytes);

		public MessageTypeId Id
			=> typeId;
	}