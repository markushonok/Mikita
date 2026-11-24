using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Mikita.Network.Domains;

public interface IDnsResolveN
	{
		IAsyncEnumerable<IPAddress> AddressesOf
			(
				string domain,
				CancellationToken cancel = default
			);
	}