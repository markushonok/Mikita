using Mikita.Routines;
using Mikita.Threading.Pools;

namespace Mikita.Test.Threading;

public static class SingleUseTaskPoolTest
	{
		[Fact]
		public static async Task RunsWorkBeforeStop()
			{
				var source = new RecordingTaskPool();
				var pool = source.WithSingleUse;

				pool.Put(NoWork);
				await pool.Stop();

				source.Puts.ShouldBe(1);
				source.Stops.ShouldBe(1);
			}

		[Fact]
		public static async Task IgnoresWorkAfterStop()
			{
				var source = new RecordingTaskPool();
				var pool = source.WithSingleUse;

				await pool.Stop();
				pool.Put(NoWork);

				source.Puts.ShouldBe(0);
			}

		private static Task NoWork(CancellationToken cancel = default)
			=> Task.CompletedTask;

		private sealed class RecordingTaskPool
			: ITaskPool
			{
				public int Puts { get; private set; }

				public int Stops { get; private set; }

				public void Put(CancellableTask task)
					=> Puts++;

				public Task Stop()
					{
						Stops++;
						return Task.CompletedTask;
					}
			}
	}