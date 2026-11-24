using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Keys;

namespace Mikita.Network.Test;

public static class PeerAliasTest
	{
		[Fact]
		public static void PointSetsReferent()
			{
				var aliases = PeerAliasMap.New;
				var alias = new PeerKey(1);
				var referent = new PeerKey(2);

				aliases.Point(alias, referent);

				aliases.ReferentOf(alias).ShouldBe(referent);
			}

		[Fact]
		public static void PointReplacesReferent()
			{
				var aliases = PeerAliasMap.New;
				var alias = new PeerKey(1);

				aliases.Point(alias, new PeerKey(2));
				aliases.Point(alias, new PeerKey(3));

				aliases.ReferentOf(alias).ShouldBe(new PeerKey(3));
			}

		[Fact]
		public static void SeveralAliasesShareTarget()
			{
				var aliases = PeerAliasMap.New;
				var referent = new PeerKey(7);

				aliases.Point(new PeerKey(1), referent);
				aliases.Point(new PeerKey(2), referent);

				aliases.ReferentOf(new PeerKey(1)).ShouldBe(referent);
				aliases.ReferentOf(new PeerKey(2)).ShouldBe(referent);
			}

		[Fact]
		public static void RemoveDropsAlias()
			{
				var aliases = PeerAliasMap.New;
				var alias = new PeerKey(1);
				aliases.Point(alias, new PeerKey(2));

				aliases.Remove(alias);

				aliases.ReferentOf(alias).ShouldBeNull();
			}

		[Fact]
		public static void RemoveUnpointedAliasIsIgnored()
			{
				var aliases = PeerAliasMap.New;

				aliases.Remove(new PeerKey(1));

				aliases.ReferentOf(new PeerKey(1)).ShouldBeNull();
			}

		[Fact]
		public static void UnknownAliasIsNull()
			{
				var aliases = PeerAliasMap.New;

				aliases.ReferentOf(new PeerKey(1)).ShouldBeNull();
			}
	}