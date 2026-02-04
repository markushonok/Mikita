using System.Runtime.CompilerServices;

namespace Mikita.Threading;

public interface IAwaiter
	: ICriticalNotifyCompletion
	{
		bool IsCompleted { get; }

		void GetResult();
	}

public interface IAwaiter<out T>
	: IAwaiter
	{
		new T GetResult();
	}