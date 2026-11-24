using Mikita.Steps;

namespace Mikita.Network.Running;

public static class PeerRunStep
	{
		extension(IPeerRun run)
			{
				public AsyncStep AsStep
					=> new
						(
							@do: run.Start,
							undo: run.Stop
						);
			}
	}