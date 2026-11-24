using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using System.Net;

namespace Mikita.Network.Test;

public static class PeerKeyLifecycleTest
	{
		[Fact]
		public static void EndpointComparerHashesEqualEndpointsConsistently()
			{
				var parsed = new IPEndPoint
					(
						IPAddress.Parse("127.0.0.1"),
						50000
					);
				var bytes = new IPEndPoint
					(
						new IPAddress([127, 0, 0, 1]),
						50000
					);

				EndpointKeyComparer.Instance
					.Equals(parsed, bytes)
					.ShouldBeTrue();

				EndpointKeyComparer.Instance
					.GetHashCode(parsed)
					.ShouldBe
						(
							EndpointKeyComparer.Instance
								.GetHashCode(bytes)
						);
			}

		[Fact]
		public static void EndpointRegistryReusesKeyForEqualEndpoints()
			{
				var registry = PeerRegistry<IPEndPoint>
					.With(PeerKeyPool.New)
					.WithReferenceCountBy
						(
							EndpointKeyComparer.Instance
						);

				var parsed = registry.ManualKeyFor
					(
						new IPEndPoint
							(
								IPAddress.Parse("127.0.0.1"),
								50000
							)
					);
				var bytes = registry.ManualKeyFor
					(
						new IPEndPoint
							(
								new IPAddress([127, 0, 0, 1]),
								50000
							)
					);

				bytes.ShouldBe(parsed);
			}

		[Fact]
		public static void PoolRejectsUnacquiredKeyRelease()
			{
				var key = new PeerKey(7);

				var exception = Should.Throw<InvalidOperationException>
					(
						() => PeerKeyPool.New.Release(key)
					);

				exception.Message.ShouldContain(key.ToString());
			}

		[Fact]
		public static void RegisteredPeerRemainsMappedUntilLastRelease()
			{
				var registry = PeerRegistry<string>
					.With(PeerKeyPool.New)
					.WithReferenceCount;

				var first = registry.ManualKeyFor("peer");
				var second = registry.ManualKeyFor("peer");

				second.ShouldBe(first);
				registry.Release(first);
				registry.PeerWith(first).ShouldBe("peer");
				registry.Release(second);
				registry.PeerWith(first).ShouldBeNull();
			}

		[Fact]
		public static void RegistryRejectsUnknownKeyRelease()
			{
				var registry = new PeerRegistry<string>
					(
						PeerKeyPool.New,
						new Dictionary<PeerKey, string>()
					);
				var key = new PeerKey(11);

				var exception = Should.Throw<InvalidOperationException>
					(
						() => registry.Release(key)
					);

				exception.Message.ShouldContain(key.ToString());
			}

		[Fact]
		public static void ReferenceCountRejectsRepeatedFinalRelease()
			{
				var registry = PeerRegistry<string>
					.With(PeerKeyPool.New)
					.WithReferenceCount;
				var key = registry.ManualKeyFor("peer");
				registry.Release(key);

				var exception = Should.Throw<InvalidOperationException>
					(
						() => registry.Release(key)
					);

				exception.Message.ShouldContain(key.ToString());
			}

		[Fact]
		public static void PeerTypesShareUniqueKeys()
			{
				var keys = PeerKeyPool.New;
				var textPeers = PeerRegistry<string>.With(keys);
				var uriPeers = PeerRegistry<Uri>.With(keys);

				var textKey = textPeers.ManualKeyFor("peer");
				var uriKey = uriPeers.ManualKeyFor
					(
						new Uri("https://example.com")
					);

				uriKey.ShouldNotBe(textKey);
			}
	}