using Mikita.Logging;

namespace Mikita.Test.Logging;

public static class MultiLogTest
	{
		[Fact]
		public static void WritesToEveryAddedLog()
			{
				var log = MultiLog.NewEmpty;
				var first = new CollectingLog();
				var second = new CollectingLog();
				log.Add(first);
				log.Add(second);

				log.Write("message");

				first.Messages.ShouldHaveSingleItem().ShouldBe("message");
				second.Messages.ShouldHaveSingleItem().ShouldBe("message");
			}

		[Fact]
		public static void StopsWritingToRemovedLog()
			{
				var log = MultiLog.NewEmpty;
				var first = new CollectingLog();
				var second = new CollectingLog();
				log.Add(first);
				log.Add(second);

				log.Remove(first);
				log.Write("message");

				first.Messages.ShouldBeEmpty();
				second.Messages.ShouldHaveSingleItem().ShouldBe("message");
			}

		[Fact]
		public static async Task SerialAccessSerializesWritesAndAdds()
			{
				var inner = new BlockingMultiLog();
				var log = inner.WithSerialAccess;
				var writing = Task.Run(() => log.Write("message"));
				inner.WriteEntered.Wait();

				var adding = Task.Run(() => log.Add(new CollectingLog()));
				await Task.Delay(50);
				inner.AddEntered.IsSet.ShouldBeFalse();

				inner.ContinueWrite.Set();
				await Task.WhenAll(writing, adding);
				inner.AddEntered.IsSet.ShouldBeTrue();
			}

		private sealed class CollectingLog: ILog
			{
				public ICollection<string> Messages { get; } = [];

				public void Write(string message)
					=> Messages.Add(message);
			}

		private sealed class BlockingMultiLog: IMultiLog
			{
				public ManualResetEventSlim AddEntered { get; } = new();

				public ManualResetEventSlim ContinueWrite { get; } = new();

				public ManualResetEventSlim WriteEntered { get; } = new();

				public void Add(ILog log)
					=> AddEntered.Set();

				public void Remove(ILog log) {}

				public void Write(string message)
					{
						WriteEntered.Set();
						ContinueWrite.Wait();
					}
			}
	}