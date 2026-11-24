using System;

namespace Mikita.Network.Packets.Senders;

public interface IPacketSender<in TRecipient>
	{
		void Send
			(
				TRecipient recipient,
				ReadOnlySpan<byte> packet
			);
	}