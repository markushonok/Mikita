using System.Threading;

namespace Mikita.Network.Requests.Registries;

public static class SerialRequestRegistryInstancing
	{
		extension(IRequestRegistry registry)
			{
				/// <summary>
				/// Adds serialized public access. The source must no longer be
				/// accessed directly.
				/// </summary>
				public IRequestRegistry WithSerialAccess
					=> new SerialRequestRegistry
						(
							registry,
							new Lock()
						);
			}
	}