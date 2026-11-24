using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Domains;

public static class DnsShortcut
	{
		extension(IDnsResolve1 dns)
			{
				public async Task<IPEndPoint?> EndpointOf
					(
						Uri uri,
						CancellationToken cancel = default
					)
					{
						var address = await dns.AddressOf(uri, cancel);

						return address is object
							? new IPEndPoint(address, uri.Port)
							: null;
					}

				public Task<IPAddress?> AddressOf
					(
						Uri uri,
						CancellationToken cancel = default
					)
					=> dns.AddressOf(uri.Host, cancel);
			}
	}