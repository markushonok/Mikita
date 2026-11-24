using Mikita.Network.Peers.Channels;
using System.Net;

namespace Mikita.Network.Test;

public static class PeerChannelSharingTest
	{
		[Fact]
		public static void SecondOpenSharesChannel()
			{
				var source = new StubChannelSource();
				var registry = RegistryWith(source);

				var first = registry.Open(Loopback);
				var second = registry.Open(Loopback);

				first.Id.ShouldBe(second.Id);
				first.Key.ShouldBe(second.Key);
				source.Created.ShouldBe(1);
			}

		[Fact]
		public static void DistinctEndpointsHaveDistinctKeys()
			{
				var registry = RegistryWith(new StubChannelSource());

				var first = registry.Open(Loopback);
				var second = registry.Open(Other);

				first.Key.ShouldNotBe(second.Key);
			}

		[Fact]
		public static void OpenedChannelIsFoundById()
			{
				var registry = RegistryWith(new StubChannelSource());

				var opened = registry.Open(Loopback);

				registry.With(opened.Id).ShouldNotBeNull();
			}

		[Fact]
		public static void UnknownChannelIsClosed()
			{
				var registry = RegistryWith(new StubChannelSource());

				registry.With(new PeerChannelId(99)).ShouldBeNull();
			}

		[Fact]
		public static void FirstDisposeKeepsChannelOpen()
			{
				var source = new StubChannelSource();
				var registry = RegistryWith(source);
				var first = registry.Open(Loopback);
				var second = registry.Open(Loopback);

				second.Dispose();

				source.Latest.State.ShouldNotBe(PeerChannelState.Closed);
				registry.With(first.Id).ShouldNotBeNull();
			}

		[Fact]
		public static void LastDisposeClosesChannel()
			{
				var source = new StubChannelSource();
				var registry = RegistryWith(source);
				var first = registry.Open(Loopback);
				var second = registry.Open(Loopback);

				first.Dispose();
				second.Dispose();

				source.Latest.State.ShouldBe(PeerChannelState.Closed);
				registry.With(first.Id).ShouldBeNull();
			}

		[Fact]
		public static void WithReturnsCountedReference()
			{
				var source = new StubChannelSource();
				var registry = RegistryWith(source);
				var opened = registry.Open(Loopback);
				var retained = registry.With(opened.Id);
				retained.ShouldNotBeNull();

				opened.Dispose();

				source.Latest.State.ShouldNotBe(PeerChannelState.Closed);

				retained!.Dispose();

				source.Latest.State.ShouldBe(PeerChannelState.Closed);
				registry.With(opened.Id).ShouldBeNull();
			}

		[Fact]
		public static void RepeatedDisposeDoesNotReleaseTwice()
			{
				var source = new StubChannelSource();
				var registry = RegistryWith(source);
				var first = registry.Open(Loopback);
				registry.Open(Loopback);

				first.Dispose();
				first.Dispose();

				source.Latest.State.ShouldNotBe(PeerChannelState.Closed);
			}

		[Fact]
		public static async Task ConcurrentOpensShareChannel()
			{
				var source = new StubChannelSource();
				var registry = PeerChannelRegistry
					.With(source)
					.WithReferenceCount
					.WithSerialAccess;
				var handles = await Task.WhenAll
					(
						Enumerable
							.Range(0, 8)
							.Select(_ => Task.Run(() => registry.Open(Loopback)))
					);

				handles.Select(handle => handle.Id).Distinct().Count()
					.ShouldBe(1);
				source.Created.ShouldBe(1);

				foreach (var handle in handles)
					handle.Dispose();
			}

		private static IPeerChannelRegistry RegistryWith(IPeerChannelSource source)
			=> PeerChannelRegistry.With(source).WithReferenceCount;

		private static IPEndPoint Loopback
			=> new(IPAddress.Loopback, 12345);

		private static IPEndPoint Other
			=> new(IPAddress.Loopback, 12346);
	}