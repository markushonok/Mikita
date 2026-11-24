using LiteNetLib;
using Mikita.Identity;
using Mikita.Logging;
using Mikita.Network.Peers.Aliases;
using Mikita.Network.Peers.Channels;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Peers.Registries;
using Mikita.Observation.Tables;
using System.Net;
using System.Net.Sockets;

namespace Mikita.Network.Test;

public static class PeerChannelLifecycleTest
	{
		[Fact]
		public static void OpenedChannelStartsOfflineWithoutOfflineEvent()
			{
				using var client = OpenClient();
				var hook = new ChannelHook();
				client.Events.Of(new PeerChannelId(1)).Add(hook);

				using var channel = client.Channels.Open(UnusedEndpoint);

				channel.State.ShouldBe(PeerChannelState.Offline);
				hook.Offline.Task.IsCompleted.ShouldBeFalse();
			}

		[Fact]
		public static async Task OpenedChannelGoesOnline()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				using var channel = client.Channels.Open(server.Endpoint);

				await WaitFor(channel, PeerChannelState.Online);
			}

		[Fact]
		public static async Task ConnectedChannelRaisesOnlineEvent()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				using var channel = client.Channels.Open(server.Endpoint);
				var hook = new ChannelHook();

				client.Events.Of(channel.Id).Add(hook);

				await hook.Online.Task.WaitAsync(TimeSpan.FromSeconds(5));
			}

		[Fact]
		public static async Task DroppedServerGoesOffline()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				using var channel = client.Channels.Open(server.Endpoint);

				await WaitFor(channel, PeerChannelState.Online);
				server.Dispose();

				await WaitFor(channel, PeerChannelState.Offline);
			}

		[Fact]
		public static async Task DisposedChannelGoesClosed()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				var channel = client.Channels.Open(server.Endpoint);

				await WaitFor(channel, PeerChannelState.Online);
				channel.Dispose();

				channel.State.ShouldBe(PeerChannelState.Closed);
			}

		[Fact]
		public static async Task DisposedChannelRaisesCloseEvent()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				var channel = client.Channels.Open(server.Endpoint);
				var hook = new ChannelHook();
				client.Events.Of(channel.Id).Add(hook);

				channel.Dispose();

				await hook.Closed.Task.WaitAsync(TimeSpan.FromSeconds(5));
			}

		[Fact]
		public static async Task DroppedServerRaisesOfflineEvent()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				using var channel = client.Channels.Open(server.Endpoint);
				var hook = new ChannelHook();

				client.Events.Of(channel.Id).Add(hook);
				await WaitFor(channel, PeerChannelState.Online);
				server.Dispose();

				await hook.Offline.Task.WaitAsync(TimeSpan.FromSeconds(5));
			}

		[Fact]
		public static void OpenedChannelHasNoReferent()
			{
				using var client = OpenClient();
				using var channel = client.Channels.Open(UnusedEndpoint);

				client.Aliases.ReferentOf(channel.Key).ShouldBeNull();
			}

		[Fact]
		public static async Task KeyUnchangedWhileGoingOnline()
			{
				using var server = OpenServer();
				using var client = OpenClient();
				using var channel = client.Channels.Open(server.Endpoint);
				var key = channel.Key;

				await WaitFor(channel, PeerChannelState.Online);

				channel.Key.ShouldBe(key);
				var transport = client.Aliases.ReferentOf(key);

				transport.ShouldNotBeNull();
				client.Connections.PeerWith(transport!.Value)
					.ShouldNotBeNull();
			}

		[Fact]
		public static async Task KeySurvivesReconnectWithReplacementConnection()
			{
				using var server = new ReopenableServer(FreePort());
				using var client = OpenClient();
				using var channel = client.Channels.Open(server.Endpoint);
				var key = channel.Key;

				await WaitFor(channel, PeerChannelState.Online);
				var first = ConnectedPeer(client, key);
				var firstTransport = client.Aliases.ReferentOf(key);
				firstTransport.ShouldNotBeNull();

				server.Stop();
				await WaitFor(channel, PeerChannelState.Offline);

				client.Aliases.ReferentOf(key).ShouldBeNull();
				client.Connections.PeerWith(firstTransport!.Value).ShouldBeNull();

				server.Start();

				await WaitFor(channel, PeerChannelState.Online);
				var second = ConnectedPeer(client, key);

				channel.Key.ShouldBe(key);
				second.ShouldNotBeSameAs(first);
			}

		[Fact]
		public static void SeparateChannelsHaveDistinctKeys()
			{
				using var client = OpenClient();
				using var first = client.Channels
					.Open(new IPEndPoint(IPAddress.Loopback, 1));
				using var second = client.Channels
					.Open(new IPEndPoint(IPAddress.Loopback, 2));

				first.Key.ShouldNotBe(second.Key);
			}

		[Fact]
		public static void DisposedIdleChannelReleasesKey()
			{
				using var client = OpenClient();
				var channel = client.Channels.Open(UnusedEndpoint);
				var key = channel.Key;

				channel.Dispose();

				client.Aliases.ReferentOf(key).ShouldBeNull();
			}

		[Fact]
		public static void FailedOpenReleasesChannelId()
			{
				var listener = new EventBasedNetListener();
				var manager = new NetManager(listener)
					{
						UnsyncedEvents = true
					};
				manager.Start();

				try
					{
						var ids = PeerChannelIdPool.New;
						var keys = PeerKeyPool.New.WithSerialAccess;
						var source = new PeerChannelSource
							(
								listener,
								manager,
								ConnectionKey,
								new FailingKeyPool(),
								ConnectionRegistry(keys),
								PeerAliasMap.New.WithSerialAccess,
								Log.Debug,
								EventSourceMap
									<
										PeerChannelId,
										IPeerChannelHook
									>
									.NewEmpty,
								ids
							);

						Should.Throw<InvalidOperationException>
							(() => source.Open(UnusedEndpoint));

						Should.Throw<InvalidOperationException>
							(() => ids.Release(new PeerChannelId(1)));
					}
				finally
					{
						manager.Stop();
					}
			}

		private static NetPeer? ConnectedPeer
			(
				TestClient client,
				PeerKey key
			)
			{
				var transport = client.Aliases.ReferentOf(key);
				return transport is object
					? client.Connections.PeerWith(transport.Value)
					: null;
			}

		private static IPeerRegistry<NetPeer> ConnectionRegistry
			(
				IKeyPool<PeerKey> keys
			)
			=> PeerRegistry<NetPeer>
				.With(keys)
				.WithReferenceCount
				.WithSerialAccess;

		private static int FreePort()
			{
				using var socket = new UdpClient(0);
				return ((IPEndPoint) socket.Client.LocalEndPoint!).Port;
			}

		private static IPEndPoint UnusedEndpoint
			=> new(IPAddress.Loopback, 1);

		private static TestServer OpenServer()
			{
				var listener = new EventBasedNetListener();
				listener.ConnectionRequestEvent
					+= request => request.AcceptIfKey(ConnectionKey);

				var manager = new NetManager(listener)
					{
						UnsyncedEvents = true
					};
				manager.Start();
				return new TestServer(manager);
			}

		private static TestClient OpenClient()
			{
				var listener = new EventBasedNetListener();
				var manager = new NetManager(listener)
					{
						UnsyncedEvents = true
					};
				manager.Start();
				return new TestClient(manager, listener);
			}

		private static async Task WaitFor
			(
				IPeerChannel channel,
				PeerChannelState state
			)
			{
				using var deadline = new CancellationTokenSource
					(TimeSpan.FromSeconds(5));

				while (channel.State != state)
					await Task.Delay(10, deadline.Token);
			}

		private sealed class FailingKeyPool: IKeyPool<PeerKey>
			{
				public PeerKey Acquire()
					=> throw new InvalidOperationException
						("No peer key is available.");

				public void Release(PeerKey key) {}
			}

		private sealed class TestServer(NetManager manager) : IDisposable
			{
				public IPEndPoint Endpoint { get; }
					= new(IPAddress.Loopback, manager.LocalPort);

				public void Dispose()
					=> manager.Stop();
			}

		private sealed class ReopenableServer : IDisposable
			{
				public ReopenableServer(int port)
					{
						Port = port;

						var listener = new EventBasedNetListener();
						listener.ConnectionRequestEvent += request
							=> request.AcceptIfKey(ConnectionKey);

						manager = new NetManager(listener)
							{
								UnsyncedEvents = true
							};
						manager.Start(port);
					}

				public int Port { get; }

				public IPEndPoint Endpoint
					=> new(IPAddress.Loopback, Port);

				public void Stop()
					=> manager.Stop();

				public void Start()
					=> manager.Start(Port);

				public void Dispose()
					=> manager.Stop();

				private readonly NetManager manager;
			}

		private sealed class TestClient
			: IDisposable
			{
				public TestClient
					(
						NetManager manager,
						EventBasedNetListener listener
					)
					{
						this.manager = manager;

						Keys = PeerKeyPool.New.WithSerialAccess;
						Aliases = PeerAliasMap.New.WithSerialAccess;
						Connections = ConnectionRegistry(Keys);
						Channels = PeerChannelSource.With
							(
								listener,
								manager,
								ConnectionKey,
								Keys,
								Connections,
								Aliases,
								Events,
								Log.Debug
							);
					}

				public IKeyPool<PeerKey> Keys { get; }

				public IPeerAliasMap Aliases { get; }

				public IPeerRegistry<NetPeer> Connections { get; }

				public IEventSourceMap<PeerChannelId, IPeerChannelHook> Events
					{ get; } = EventSourceMap
						<
							PeerChannelId,
							IPeerChannelHook
						>
						.NewEmpty;

				public IPeerChannelSource Channels { get; }

				public void Dispose()
					=> manager.Stop();

				private readonly NetManager manager;
			}

		private sealed class ChannelHook: IPeerChannelHook
			{
				public TaskCompletionSource Online { get; }
					= new(TaskCreationOptions.RunContinuationsAsynchronously);

				public TaskCompletionSource Offline { get; }
					= new(TaskCreationOptions.RunContinuationsAsynchronously);

				public TaskCompletionSource Closed { get; }
					= new(TaskCreationOptions.RunContinuationsAsynchronously);

				void IPeerChannelHook.Open() {}

				void IPeerChannelHook.Online()
					=> Online.TrySetResult();

				void IPeerChannelHook.Offline()
					=> Offline.TrySetResult();

				public void Close()
					=> Closed.TrySetResult();
			}

		private const string ConnectionKey = "peer_channel_test";
	}