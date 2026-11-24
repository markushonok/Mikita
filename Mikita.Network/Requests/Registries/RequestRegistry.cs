using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests.Counters;
using System;
using System.Collections.Generic;

namespace Mikita.Network.Requests.Registries;

public sealed partial class RequestRegistry
	{
		public static IRequestRegistry New
			=> new RequestRegistry
				(
					RequestCounter.New,
					new Dictionary<RequestId, PeerKey>()
				);
	}

public sealed partial class RequestRegistry
	(
		IRequestCounter counter,
		IDictionary<RequestId, PeerKey> recipients
	)
	: IRequestRegistry
	{
		public RequestId Register(PeerKey recipient)
			{
				var request = counter.Next;

				return recipients.TryAdd(request, recipient)
					? request
					: throw OccupiedRequestException(request);
			}

		private static Exception OccupiedRequestException
			(
				RequestId request
			)
			=> new InvalidOperationException
				(
					$"Request `{request}` is already registered."
				);

		public PeerKey? RecipientKeyOf(RequestId request)
			=> recipients.TryGetValue(request, out var recipient)
				? recipient
				: null;

		public void Release(RequestId request)
			=> recipients.Remove(request);
	}