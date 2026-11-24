using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Keys;
using System;
using System.Buffers.Binary;

namespace Mikita.Network.Pipeline;

/// <summary>
/// Routes the complete packet by its registered role. Each destination reads
/// its own header, including the original two-byte message type ID.
/// Unknown type IDs and packets shorter than an ID go to rejected.
/// </summary>
public sealed class MessageRoleRouter
	(
		IMessageRegistry messages,
		IMiddleware requests,
		IMiddleware responses,
		IMiddleware notices,
		IMiddleware rejected
	)
	: IMiddleware
	{
		public void Handle(PeerKey peer, ReadOnlySpan<byte> bytes)
			{
				if (bytes.Length < sizeof(short))
					{
						rejected.Handle(peer, bytes);
						return;
					}

				var typeId = new MessageTypeId
					(
						BinaryPrimitives.ReadInt16LittleEndian(bytes)
					);
				var destination = messages.LabelOf(typeId)?.Role switch
					{
						MessageRole.Request => requests,
						MessageRole.Response => responses,
						MessageRole.Notice => notices,
						_ => rejected
					};

				destination.Handle(peer, bytes);
			}
	}