namespace Mikita.Threading;

public interface IFailureHook
	{
		void FailWith(string message);
	}