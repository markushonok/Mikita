using System;
using System.Threading.Tasks;

namespace Mikita.Network.Running;

public sealed class PeerRun
	(
		Func<Task> start,
		Func<Task> stop
	)
	: IPeerRun
	{

		public Task Start()
			=> start();

		public Task Stop()
			=> stop();
	}