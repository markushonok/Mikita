using LiteNetLib;
using Mikita.Network.Pipeline;
using System.Net;
using System.Threading.Tasks;

namespace Mikita.Network.Running;

public sealed class PacketReceiptRun
	(
		EventBasedNetListener listener,
		IPacketFunnel packetReceipt
	)
	: IPeerRun
	{
		public Task Start()
			{
				listener.NetworkReceiveUnconnectedEvent += Handle;
				listener.NetworkReceiveEvent += Handle;

				return Task.CompletedTask;
			}

		public Task Stop()
			{
				listener.NetworkReceiveUnconnectedEvent -= Handle;
				listener.NetworkReceiveEvent -= Handle;

				return Task.CompletedTask;
			}

		private void Handle
			(
				IPEndPoint endpoint,
				NetPacketReader reader,
				UnconnectedMessageType type
			)
			=> packetReceipt.Handle
				(
					endpoint,
					reader.GetRemainingBytesSpan()
				);

		private void Handle
			(
				NetPeer peer,
				NetPacketReader reader,
				byte channel,
				DeliveryMethod method
			)
			=> packetReceipt.Handle
				(
					peer,
					reader.GetRemainingBytesSpan()
				);
	}