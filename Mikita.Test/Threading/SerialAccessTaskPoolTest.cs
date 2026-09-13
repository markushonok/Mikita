using Mikita.Routines;
using Mikita.Structs.Referring;
using Mikita.Threading.Pools;

namespace Mikita.Test.Threading;

public static class SerialAccessTaskPoolTest
	{
		[Fact]
		public static void OverlappingStopsShareOperation()
			{
				var basePool = new ObservableTaskPool();
				var pool = new SerialAccessTaskPool
					(
						basePool,
						stopping: Ref<Task>.Null,
						access: new Lock()
					);

				var first = pool.Stop();
				var second = pool.Stop();

				second.ShouldBeSameAs(first);
				basePool.Stops.ShouldBe(1);
			}

		[Fact]
		public static async Task PutsResumeAfterStop()
			{
				var basePool = new ObservableTaskPool();
				var pool = new SerialAccessTaskPool
					(
						basePool,
						stopping: Ref<Task>.Null,
						access: new Lock()
					);

				var stopping = pool.Stop();
				pool.Put(_ => Task.CompletedTask);
				basePool.Puts.ShouldBe(0);

				basePool.CompleteStop();
				await stopping;
				pool.Put(_ => Task.CompletedTask);

				basePool.Puts.ShouldBe(1);
			}

		private sealed class ObservableTaskPool: ITaskPool
			{
				public int Puts { get; private set; }

				public int Stops { get; private set; }

				public void Put(CancellableTask task)
					=> Puts++;

				public Task Stop()
					{
						Stops++;
						return stopping.Task;
					}

				public void CompleteStop()
					=> stopping.SetResult();

				private readonly TaskCompletionSource stopping = new
					(
						TaskCreationOptions.RunContinuationsAsynchronously
					);
			}
	}