using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Mikita.Network.Domains;

public sealed class DnsResolveN
	: IDnsResolveN
	{
		public async IAsyncEnumerable<IPAddress> AddressesOf
			(
				string domain,
				[EnumeratorCancellation] CancellationToken cancel = default
			)
			{
				var addresses = await Dns.GetHostAddressesAsync(domain, cancel);
				foreach (var address in addresses) yield return address;
			}
	}