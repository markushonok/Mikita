using Mikita.Network.Peers.Keys;
using System.Threading;

namespace Mikita.Network.Requests.Registries;

/// <summary>
/// Serializes access to another <see cref="IRequestRegistry"/>.
/// </summary>
/// <param name="source">Registry receiving serialized calls.</param>
/// <param name="access">Serializes public API calls.</param>
public sealed class SerialRequestRegistry
	(
		IRequestRegistry source,
		Lock access
	)
	: IRequestRegistry
	{
		public RequestId Register(PeerKey recipient)
			{
				lock (access) return source.Register(recipient);
			}

		public PeerKey? RecipientKeyOf(RequestId request)
			{
				lock (access) return source.RecipientKeyOf(request);
			}

		public void Release(RequestId request)
			{
				lock (access) source.Release(request);
			}
	}