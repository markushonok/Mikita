using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Domains;

public interface IDnsResolve1
	{
		Task<IPAddress?> AddressOf
			(
				string domain,
				CancellationToken cancel = default
			);
	}