# Mikita.Network

**Define a binary protocol and exchange messages between peers on .NET 10.** Mikita.Network provides the connection and message plumbing over LiteNetLib. Your application defines the messages, their binary formats, and what happens when they arrive.

| Message role | Use it for |
| --- | --- |
| `Request` | Ask a peer to do something or return data. |
| `Response` | Return an outcome to the request that is waiting for it. |
| `Notice` | Tell a peer that something happened without waiting for a reply. |

## Quickstart: send a notice between two processes

The server sends a one-byte temperature reading as soon as a client connects. The client routes the packet and prints the reading. Both use the same protocol library and run on the loopback interface.

From a Mikita source checkout, create the shared library with `dotnet new classlib -n SensorProtocol -f net10.0`. Add Mikita.Network with `dotnet add SensorProtocol/SensorProtocol.csproj reference Mikita.Network/Mikita.Network.csproj`. Create two console apps with `dotnet new console -n SensorClient -f net10.0` and `dotnet new console -n SensorServer -f net10.0`. In each app, run `dotnet add reference ../SensorProtocol/SensorProtocol.csproj`.

### 1. Shared library: `SensorProtocol/Class1.cs`

Both peers register the same message and use the same one-byte format.

```csharp
using Mikita.Network.Messages;
using Mikita.Structs.Buffers.Writing;
using System;

namespace Sensors;

public static class SensorProtocol
	{
		public static void WriteCelsius(RentedByteWriter writer, byte celsius)
			=> writer.Put(celsius);

		public static byte? ReadCelsius(ReadOnlySpan<byte> body)
			=> body.Length == 1 ? body[0] : null;

		public const string ConnectionKey = "sensor-demo";

		public const int Port = 9050;

		public static readonly MessageLabel Reading
			= new
				(
					TypeName: "sensor.reading",
					MessageRole.Notice
				);
	}
```

### 2. Client: `SensorClient/Program.cs`

This protocol has one notice, so the client only needs a type router.

```csharp
using LiteNetLib;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Peers.Keys;
using Mikita.Network.Pipeline;
using Sensors;
using System;
using System.Net;

var reading = MessageRegistry.New.Register(SensorProtocol.Reading);
var router = MessageTypeRouter.New;
router.Add(new ReadingReceipt().With(reading.TypeId));

var listener = new EventBasedNetListener();
listener.NetworkReceiveEvent += (peer, reader, channel, method)
	=> router.Handle(new PeerKey(peer.Id), reader.GetRemainingBytesSpan());

var client = new NetManager(listener)
	{
		UnsyncedEvents = true,
		AutoRecycle = true
	};
if (!client.Start())
	throw new InvalidOperationException("Could not start the client.");

var server = new IPEndPoint(IPAddress.Loopback, SensorProtocol.Port);
client.Connect(server, SensorProtocol.ConnectionKey);
Console.ReadLine();
client.Stop();

sealed class ReadingReceipt: IMiddleware
	{
		public void Handle(PeerKey peer, ReadOnlySpan<byte> body)
			{
				if (SensorProtocol.ReadCelsius(body) is byte celsius)
					Console.WriteLine($"Received {celsius} °C");
			}
	}
```

### 3. Server: `SensorServer/Program.cs`

The server accepts the shared connection key and sends the notice through the newly connected peer.

```csharp
using LiteNetLib;
using Mikita.Network.Messages.Registries;
using Mikita.Network.Notices;
using Mikita.Network.Packets.Senders;
using Mikita.Network.Peers.Keys;
using Sensors;
using System;
using System.Net;

var messages = MessageRegistry.New;
var reading = messages.Register(SensorProtocol.Reading);
var listener = new EventBasedNetListener();
listener.ConnectionRequestEvent += request
	=> request.AcceptIfKey(SensorProtocol.ConnectionKey);

listener.PeerConnectedEvent += peer =>
	{
		var sender = NoticeSender.With(messages, new ConnectedPeer(peer));
		var peerKey = new PeerKey(peer.Id);
		var celsius = (byte) 21;
		sender.Send(peerKey, reading.TypeId, celsius, SensorProtocol.WriteCelsius);
	};

var server = new NetManager(listener)
	{
		UnsyncedEvents = true
	};
var port = SensorProtocol.Port;
if (!server.Start(IPAddress.Loopback, IPAddress.IPv6Loopback, port))
	throw new InvalidOperationException("Could not start the server.");

Console.WriteLine($"Listening on 127.0.0.1:{port}.");
Console.ReadLine();
server.Stop();

sealed class ConnectedPeer(NetPeer peer): IPacketSender<PeerKey>
	{
		public void Send(PeerKey recipient, ReadOnlySpan<byte> packet)
			{
				if (recipient.Number != peer.Id)
					throw new ArgumentException("The recipient is not this peer.");

				peer.Send(packet, DeliveryMethod.ReliableOrdered);
			}
	}
```

From the checkout root, start the server with `dotnet run --project SensorServer`, then run `dotnet run --project SensorClient` in another terminal. The client prints `Received 21 °C`. Press Enter in each terminal to stop the program.

## Extend the example

Use `PeerChannelSource` for outgoing connections and accept the same application connection key on the listening peer through LiteNetLib. The channel reuses that key on reconnect. Use `RoutedPacketSender` to send to a `PeerKey` and `PacketFunnel` to feed received packets into your routers.

When you add requests and responses, put `MessageRoleRouter` in front of the notice router. It selects the pipeline by role; `MessageTypeRouter` selects the handler by ID and passes it the body. Requests and responses carry a separate `RequestId` for correlation; notice bodies do not.

The application decides who may subscribe to notices and what to do about missed notices after a reconnect.

## Keep peers compatible

- Share one ordered registration list between peers. The registry assigns two-byte IDs in first registration order. Message names and roles are local metadata.
- Complete registration before network use. On each peer, give its senders and routers the same registry.
- Append new labels to keep existing IDs stable, and preserve the binary formats of existing messages. The library does not negotiate versions or message lists, so version checks belong to the application.

Run the network tests from the repository root with `dotnet test Mikita.Network.Test/Mikita.Network.Test.csproj`.