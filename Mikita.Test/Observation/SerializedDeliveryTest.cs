using Mikita.Observation.Subscriptions;
using Mikita.Observation.Tables;

namespace Mikita.Test.Observation;

public static class SerializedDeliveryTest
	{
		[Fact]
		public static async Task RemovalWaitsForDeliveryBeforeResourceDisposal()
			{
				var events = EventSourceMap<int, Action>.NewEmpty.WithSerialAccess;
				var entered = NewCompletion;
				var removing = NewCompletion;
				using var finish = new ManualResetEventSlim();
				var disposed = false;
				var calls = 0;
				var subscription = events.Of(1).SubscriptionOf
					(
						() =>
							{
								entered.SetResult();
								finish.Wait(TimeSpan.FromSeconds(5)).ShouldBeTrue();
								disposed.ShouldBeFalse();
								calls++;
							}
					);
				subscription.Activate();
				var delivery = Task.Run
					(
						() => events.Of(1).Raise(action => action())
					);
				await entered.Task.WaitAsync(TimeSpan.FromSeconds(5));
				var release = Task.Run
					(
						() =>
							{
								removing.SetResult();
								subscription.Deactivate();
								disposed = true;
							}
					);
				await removing.Task.WaitAsync(TimeSpan.FromSeconds(5));
				release.IsCompleted.ShouldBeFalse();
				finish.Set();
				await Task.WhenAll(delivery, release)
					.WaitAsync(TimeSpan.FromSeconds(5));
				events.Of(1).Raise(action => action());
				calls.ShouldBe(1);
			}

		private static TaskCompletionSource NewCompletion
			=> new(TaskCreationOptions.RunContinuationsAsynchronously);
	}