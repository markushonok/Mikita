using Mikita.Network.Peers.Keys;
using System.Threading;

namespace Mikita.Network.Peers.Aliases;

/// <summary>
/// Serializes every operation of a <see cref="PeerAliasMap"/>
/// so pointing, removing, and resolving stay atomic under
/// concurrent packet handling and transport replacement.
/// </summary>
public sealed class SerialPeerAliasMap
	(
		IPeerAliasMap source,
		Lock access
	)
	: IPeerAliasMap
	{
		public void Point
			(
				PeerKey alias,
				PeerKey referent
			)
			{
				lock (access) source.Point(alias, referent);
			}

		public void Remove(PeerKey alias)
			{
				lock (access) source.Remove(alias);
			}

		public PeerKey? ReferentOf(PeerKey alias)
			{
				lock (access) return source.ReferentOf(alias);
			}
	}