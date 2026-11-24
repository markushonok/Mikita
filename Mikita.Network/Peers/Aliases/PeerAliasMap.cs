using Mikita.Network.Peers.Keys;
using System.Collections.Generic;

namespace Mikita.Network.Peers.Aliases;

public sealed partial class PeerAliasMap
	{
		/// <summary>
		/// Creates an unsynchronized alias map with fresh state.
		/// </summary>
		public static IPeerAliasMap New
			=> new PeerAliasMap
				(
					referents: new Dictionary<PeerKey, PeerKey>()
				);
	}

public sealed partial class PeerAliasMap
	(
		IDictionary<PeerKey, PeerKey> referents
	)
	: IPeerAliasMap
	{
		public void Point
			(
				PeerKey alias,
				PeerKey referent
			)
			=> referents[alias] = referent;

		public void Remove(PeerKey alias)
			=> referents.Remove(alias);

		public PeerKey? ReferentOf(PeerKey alias)
			=> referents.TryGetValue(alias, out var referent)
				? referent
				: null;
	}