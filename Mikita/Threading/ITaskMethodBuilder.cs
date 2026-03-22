using System;
using System.Runtime.CompilerServices;

namespace Mikita.Threading;

public struct ITaskMethodBuilder<T>
	{
		private AsyncTaskMethodBuilder<T> builder;

		public static ITaskMethodBuilder<T> Create()
			=> new()
				{
					builder = AsyncTaskMethodBuilder<T>.Create()
				};

		public void Start<TStateMachine>
			(
				ref TStateMachine stateMachine
			)
			where TStateMachine: IAsyncStateMachine
			=> builder.Start(ref stateMachine);

		public void SetStateMachine
			(
				IAsyncStateMachine stateMachine
			)
			=> builder.SetStateMachine(stateMachine);

		public void SetResult
			(
				T result
			)
			=> builder.SetResult(result);

		public void SetException
			(
				Exception exception
			)
			=> builder.SetException(exception);

		public ITask<T> Task => builder.Task.AsITask;

		public void AwaitOnCompleted<TAwaiter, TStateMachine>
			(
				ref TAwaiter awaiter,
				ref TStateMachine stateMachine
			)
			where TAwaiter: INotifyCompletion
			where TStateMachine: IAsyncStateMachine
			=> builder.AwaitOnCompleted(ref awaiter, ref stateMachine);

		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>
			(
				ref TAwaiter awaiter,
				ref TStateMachine stateMachine
			)
			where TAwaiter: ICriticalNotifyCompletion
			where TStateMachine: IAsyncStateMachine
			=> builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
	}