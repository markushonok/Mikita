using LiteNetLib;
using System;

namespace Mikita.Network.Packets.Senders;

public sealed class PeerPacketSender: IPacketSender<NetPeer>
	{
		public void Send
			(
				NetPeer recipient,
				ReadOnlySpan<byte> packet
			)
			=> recipient.Send(packet, DeliveryMethod.ReliableOrdered);
	}