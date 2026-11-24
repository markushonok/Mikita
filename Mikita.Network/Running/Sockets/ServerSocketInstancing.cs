using LiteNetLib;
using System;

namespace Mikita.Network.Running.Sockets;

public static class ServerSocketInstancing
	{
		extension(PeerSocketRun)
			{
				public static IPeerRun With
					(
						Func<NetManager> netManager
					)
					=> PeerSocketRun.With
						(
							netManager,
							port: 0
						);

				public static IPeerRun With
					(
						Func<NetManager> netManager,
						int port
					)
					=> PeerSocketRun.With
						(
							netManager,
							() => port
						);

				public static IPeerRun With
					(
						Func<NetManager> netManager,
						Func<int> port
					)
					=> new PeerSocketRun(netManager, port);
			}
	}