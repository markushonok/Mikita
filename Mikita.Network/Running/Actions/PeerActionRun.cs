using Mikita.Structs.Referring;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Running.Actions;

public sealed class PeerActionRun
	(
		ThreadStart action,
		IRef<bool> isRunning,
		IRef<Thread?> thread
	)
	: IStatedPeerRun
	{
		public Task Start()
			{
				isRunning.SetTo(true);
				thread.SetTo(NewThread);
				thread.Value!.Start();
				return Task.CompletedTask;
			}

		private Thread NewThread
			=> new(action)
				{
					Name = "Peer network"
				};

		public Task Stop()
			{
				isRunning.SetTo(false);
				thread.PullOut(out var value);
				return Task.Run(value!.Join);
			}

		public bool IsRunning
			=> isRunning.Value;
	}