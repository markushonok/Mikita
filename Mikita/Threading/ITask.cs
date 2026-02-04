namespace Mikita.Threading;

public interface ITask
	{
		IAwaiter GetAwaiter();
	}

public interface ITask<out T>
	: ITask
	{
		new IAwaiter<T> GetAwaiter();
	}