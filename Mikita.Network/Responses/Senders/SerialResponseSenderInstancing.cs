using System.Threading;

namespace Mikita.Network.Responses.Senders;

public static class SerialResponseSenderInstancing
	{
		extension(IResponseSender sender)
			{
				/// <summary>
				/// Adds serialized public access. The source must no longer be
				/// accessed directly.
				/// </summary>
				public IResponseSender WithSerialAccess
					=> new SerialResponseSender
						(
							sender,
							new Lock()
						);
			}
	}