using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Peers.Aliases;

/// <summary>
/// Maps an alias peer key to a referent peer key. The mapping is
/// one-way: several aliases may share one referent. An alias is
/// resolved only where its owner needs it, such as resolving a
/// session key to the connection that currently carries it.
/// </summary>
public interface IPeerAliasMap
	{
		/// <summary>
		/// Points <paramref name="alias"/> at
		/// <paramref name="referent"/>, replacing any previous referent.
		/// </summary>
		void Point(PeerKey alias, PeerKey referent);

		/// <summary>
		/// Removes <paramref name="alias"/>. Removing an alias that is
		/// not pointed anywhere is ignored.
		/// </summary>
		void Remove(PeerKey alias);

		/// <summary>
		/// The referent of <paramref name="alias"/>, or null when the
		/// alias is not pointed anywhere.
		/// </summary>
		PeerKey? ReferentOf(PeerKey alias);
	}