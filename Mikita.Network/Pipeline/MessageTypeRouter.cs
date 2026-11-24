using Mikita.Network.Messages;
using Mikita.Network.Peers.Keys;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace Mikita.Network.Pipeline;

public sealed partial class MessageTypeRouter
	{
		public static IMessageTypeRouter New
			=> NewWith(Middleware.Idle);

		public static IMessageTypeRouter NewWith(IMiddleware typeMiss)
			=> new MessageTypeRouter
				(
					new Dictionary<MessageTypeId, IMiddleware>(),
					typeMiss
				);
	}

public sealed partial class MessageTypeRouter
	(
		IDictionary<MessageTypeId, IMiddleware> dictionary,
		IMiddleware typeMiss
	)
	: IMessageTypeRouter
	{
		public void Handle(PeerKey peer, ReadOnlySpan<byte> bytes)
			{
				if (bytes.Length < sizeof(short))
					{
						typeMiss.Handle(peer, bytes);
						return;
					}

				var typeId = new MessageTypeId
					(
						BinaryPrimitives.ReadInt16LittleEndian(bytes)
					);

				if (!dictionary.TryGetValue(typeId, out var middleware))
					{
						typeMiss.Handle(peer, bytes);
						return;
					}

				middleware.Handle(peer, bytes[sizeof(short)..]);
			}

		public void Add(IMessageMiddleware middleware)
			{
				if (!dictionary.TryAdd(middleware.Id, middleware))
					throw new ArgumentException
						(
							$"Message type {middleware.Id.Value} is already registered."
						);
			}

		public void Remove(MessageTypeId typeId)
			{
				if (!dictionary.Remove(typeId))
					throw new InvalidOperationException
						(
							$"Unknown message type {typeId.Value}."
						);
			}
	}