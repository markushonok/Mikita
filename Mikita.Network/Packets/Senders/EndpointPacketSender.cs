using LiteNetLib;
using System;
using System.Net;

namespace Mikita.Network.Packets.Senders;

public sealed class EndpointPacketSender
	(
		NetManager manager
	)
	: IPacketSender<IPEndPoint>
	{
		public void Send
			(
				IPEndPoint recipient,
				ReadOnlySpan<byte> packet
			)
			{
				var isSent = manager.SendUnconnectedMessage(packet, recipient);
				if (!isSent) throw NotSentException(recipient);
			}

		private static Exception NotSentException
			(
				IPEndPoint endpoint
			)
			=> new InvalidOperationException
				(
					$"Failed to send unconnected"
					+ $" message to {endpoint}"
				);
	}