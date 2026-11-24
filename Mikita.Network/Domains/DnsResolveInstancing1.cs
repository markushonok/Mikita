namespace Mikita.Network.Domains;

public static class DnsResolveInstancing1
	{
		extension(DnsResolve1)
			{
				public static IDnsResolve1 Safe
					=> Safe;
			}

		private static readonly IDnsResolve1 Safe
			= new DnsResolve1(DnsResolveN.Instance.AsSafe);
	}