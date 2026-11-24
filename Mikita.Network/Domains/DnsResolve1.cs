using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Domains;

public sealed class DnsResolve1
	(
		IDnsResolveN source
	)
	: IDnsResolve1
	{
		public async Task<IPAddress?> AddressOf
			(
				string domain,
				CancellationToken cancel = default
			)
			{
				var ips = source.AddressesOf(domain, cancel);

				return await ips.FirstOrDefaultAsync
					(
						x => x.AddressFamily == AddressFamily.InterNetwork,
						cancel
					);
			}
	}