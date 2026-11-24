namespace Mikita.Network.Domains;

public static class DnsResolveInstancing
	{
		extension(DnsResolveN)
			{
				public static IDnsResolveN Instance
					=> Instance;
			}

		extension(IDnsResolveN source)
			{
				public IDnsResolveN AsSafe
					=> new SafeDnsResolveN(source);
			}

		private static readonly IDnsResolveN Instance
			= new DnsResolveN();
	}