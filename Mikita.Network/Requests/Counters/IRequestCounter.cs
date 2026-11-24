namespace Mikita.Network.Requests.Counters;

public interface IRequestCounter
	{
		RequestId Next { get; }
	}