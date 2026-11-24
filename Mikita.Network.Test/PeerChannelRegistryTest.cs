using Mikita.Network.Peers.Channels;
using System.Net;

namespace Mikita.Network.Test;

public static class PeerChannelRegistryTest
	{
		[Fact]
		public static void OpenedChannelIsFoundById()
			{
				var registry = PeerChannelRegistry.With(new StubChannelSource());

				var opened = registry.Open(Loopback);

				registry.With(opened.Id).ShouldNotBeNull();
			}

		[Fact]
		public static void UnknownChannelIsClosed()
			{
				var registry = PeerChannelRegistry.With(new StubChannelSource());

				registry.With(new PeerChannelId(99)).ShouldBeNull();
			}

		[Fact]
		public static void DisposalRemovesFromRegistry()
			{
				var registry = PeerChannelRegistry.With(new StubChannelSource());
				var opened = registry.Open(Loopback);

				opened.Dispose();

				registry.With(opened.Id).ShouldBeNull();
			}

		[Fact]
		public static void CreationDelegatesToSource()
			{
				var source = new StubChannelSource();
				var registry = PeerChannelRegistry.With(source);

				registry.Open(Loopback);

				source.Requested.ShouldBe(Loopback);
			}

		private static IPEndPoint Loopback
			=> new(IPAddress.Loopback, 12345);
	}