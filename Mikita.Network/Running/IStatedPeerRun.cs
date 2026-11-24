namespace Mikita.Network.Running;

public interface IStatedPeerRun: IPeerRun
	{
		bool IsRunning { get; }
	}