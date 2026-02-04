using System;
using System.Runtime.CompilerServices;

namespace Mikita.Threading;

public sealed class TaskAwaiterAdapter
	(
		TaskAwaiter source
	)
	: IAwaiter
	{
		public void OnCompleted(Action continuation)
			=> source.OnCompleted(continuation);

		public void UnsafeOnCompleted(Action continuation)
			=> source.UnsafeOnCompleted(continuation);

		public bool IsCompleted
			=> source.IsCompleted;

		public void GetResult()
			=> source.GetResult();
	}

public sealed class TaskAwaiterAdapter<T>
	(
		TaskAwaiter<T> source
	)
	: IAwaiter<T>
	{
		public void OnCompleted(Action continuation)
			=> source.OnCompleted(continuation);

		public void UnsafeOnCompleted(Action continuation)
			=> source.UnsafeOnCompleted(continuation);

		public bool IsCompleted
			=> source.IsCompleted;

		void IAwaiter.GetResult()
			=> GetResult();

		public T GetResult()
			=> source.GetResult();
	}