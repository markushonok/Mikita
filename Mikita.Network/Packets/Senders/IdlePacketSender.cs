using System;

namespace Mikita.Network.Packets.Senders;

public sealed partial class IdlePacketSender<T>
	{
		public static IPacketSender<T> Instance { get; }
			= new IdlePacketSender<T>();
	}

public sealed partial class IdlePacketSender<T>
	: IPacketSender<T>
	{
		public void Send
			(
				T recipient,
				ReadOnlySpan<byte> packet
			)
			{}
	}