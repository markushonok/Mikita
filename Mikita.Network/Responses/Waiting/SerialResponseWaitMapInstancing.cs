using System.Threading;

namespace Mikita.Network.Responses.Waiting;

public static class SerialResponseWaitMapInstancing
	{
		extension(IResponseWaitMap map)
			{
				/// <summary>
				/// Adds serialized public access. The source must no longer be
				/// accessed directly.
				/// </summary>
				public IResponseWaitMap WithSerialAccess
					=> new SerialResponseWaitMap
						(
							map,
							new Lock()
						);
			}
	}