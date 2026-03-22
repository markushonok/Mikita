using System.Runtime.CompilerServices;

namespace Mikita.Threading;

public interface ITask
	{
		IAwaiter GetAwaiter();
	}

[AsyncMethodBuilder(typeof(ITaskMethodBuilder<>))]
public interface ITask<out T>
	: ITask
	{
		new IAwaiter<T> GetAwaiter();
	}