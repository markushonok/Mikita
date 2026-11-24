using LiteNetLib;
using System;
using System.Threading.Tasks;

namespace Mikita.Network.Running.Sockets;

public sealed class PeerSocketRun
	(
		Func<NetManager> manager,
		Func<int> port
	)
	: IPeerRun
	{
		public Task Start()
			=> manager().Start(port())
				? Task.CompletedTask
				: throw BusyPortException(port());

		public static Exception BusyPortException
			(
				int port
			)
			=> new InvalidOperationException
				(
					$"Port {port} is already in use."
				);

		public Task Stop()
			{
				manager().Stop();
				return Task.CompletedTask;
			}
	}