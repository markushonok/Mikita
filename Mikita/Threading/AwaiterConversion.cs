using System.Runtime.CompilerServices;

namespace Mikita.Threading;

public static class AwaiterConversion
	{
		extension<T>(TaskAwaiter<T> source)
			{
				public TaskAwaiterAdapter<T> AsITaskAwaiter
					=> new(source);
			}

		extension(TaskAwaiter source)
			{
				public TaskAwaiterAdapter AsITaskAwaiter
					=> new(source);
			}
	}