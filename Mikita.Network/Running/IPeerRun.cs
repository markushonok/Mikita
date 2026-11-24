using System.Threading.Tasks;

namespace Mikita.Network.Running;

public interface IPeerRun
	{
		Task Start();

		Task Stop();
	}