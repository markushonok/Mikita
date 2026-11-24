using LiteNetLib;
using System;
using System.Net;

namespace Mikita.Network.Test;

/// <summary>
/// Produces real outgoing LiteNetLib peers so routing
/// tests can attach genuine <see cref="NetPeer"/>
/// instances without running a listening server.
/// </summary>
internal sealed class OutgoingPeers: IDisposable
	{
		public OutgoingPeers()
			{
				listener = new EventBasedNetListener();
				manager = new NetManager(listener);
				manager.Start();
			}

		public NetPeer At(int port)
			=> manager.Connect
				(
					new IPEndPoint(IPAddress.Loopback, port),
					key: "outgoing_peer_test"
				)
				?? throw new InvalidOperationException
					("No outgoing peer was created.");

		public void Dispose()
			=> manager.Stop();

		private readonly EventBasedNetListener listener;
		private readonly NetManager manager;
	}