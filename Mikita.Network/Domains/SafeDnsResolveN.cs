using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Mikita.Network.Domains;

public sealed class SafeDnsResolveN
	(
		IDnsResolveN source
	)
	: IDnsResolveN
	{
		public async IAsyncEnumerable<IPAddress> AddressesOf
			(
				string domain,
				[EnumeratorCancellation] CancellationToken cancel = default
			)
			{
				var enumerator = source
					.AddressesOf(domain, cancel)
					.GetAsyncEnumerator(cancel);

				while (true)
					{
						try
							{
								if (!await enumerator.MoveNextAsync()) break;
							}
						catch (SocketException)
							{
								yield break;
							}

						yield return enumerator.Current;
					}
			}
	}