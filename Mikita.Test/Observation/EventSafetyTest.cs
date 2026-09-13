using Mikita.Logging;
using Mikita.Observation.Events;

namespace Mikita.Test.Observation;

public static class EventSafetyTest
	{
		[Fact]
		public static async Task ConcurrentCallsDoNotOverlap()
			{
				var baseEvent = new BlockingEvent();
				var source = baseEvent.WithSerialAccess;
				var raising = Task.Run(() => source.Raise(_ => {}));
				baseEvent.RaiseEntered.Wait();

				var adding = Task.Run(() => source.Add(delegate {}));
				await Task.Delay(50);
				baseEvent.AddEntered.IsSet.ShouldBeFalse();

				baseEvent.ContinueRaise.Set();
				await Task.WhenAll(raising, adding);
				baseEvent.AddEntered.IsSet.ShouldBeTrue();
			}

		[Fact]
		public static void FailedReactionDoesNotStopRaise()
			{
				var calls = 0;
				var log = new CollectingLog();
				var source = Event.NewEmpty.WithSafeRaise(log);
				source.Add(() => throw new InvalidOperationException("Failed"));
				source.Add(() => calls++);

				source.Raise(reaction => reaction());

				calls.ShouldBe(1);
				log.Messages.ShouldHaveSingleItem()
					.ShouldContain("InvalidOperationException: Failed");
			}

		private sealed class BlockingEvent: IEventSource<Action>
			{
				public ManualResetEventSlim AddEntered { get; } = new();

				public ManualResetEventSlim ContinueRaise { get; } = new();

				public ManualResetEventSlim RaiseEntered { get; } = new();

				public void Add(Action reaction)
					=> AddEntered.Set();

				public void Remove(Action reaction) {}

				public void Raise(Action<Action> pattern)
					{
						RaiseEntered.Set();
						ContinueRaise.Wait();
					}
			}

		private sealed class CollectingLog: ILog
			{
				public ICollection<string> Messages { get; } = [];

				public void Write(string message)
					=> Messages.Add(message);
			}
	}