using System.Threading;

namespace Mikita.Network.Requests.Senders;

public static class SerialRequestSenderInstancing
	{
		extension(IRequestSender sender)
			{
				/// <summary>
				/// Adds serialized public access. The source must no longer be
				/// accessed directly.
				/// </summary>
				public IRequestSender WithSerialAccess
					=> new SerialRequestSender
						(
							sender,
							new Lock()
						);
			}
	}