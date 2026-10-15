using Mikita.Routines;
using Mikita.Steps;
using Mikita.Steps.Scopes;
using Mikita.Structs.Referring;

namespace Mikita.Test.Steps;

public static class ScopedStepTest
	{
		[Fact]
		public static async Task NullFromAsyncInitializerReachesThePattern()
			{
				var received = new List<string?>();
				Func<Task<string?>> initialize = () => Task.FromResult<string?>(null);
				var step = Step.NewScoped
					(
						initialize,
						value =>
							{
								received.Add(value);
								return Nothing();
							}
					);

				await step.Do();
				await step.Undo();

				received.Count.ShouldBe(1);
				received[0].ShouldBeNull();
			}

		[Fact]
		public static async Task NullFromSyncInitializerReachesThePattern()
			{
				var received = new List<string?>();
				Func<string?> initialize = () => null;
				var step = Step.NewScoped
					(
						initialize,
						value =>
							{
								received.Add(value);
								return Nothing();
							}
					);

				await step.Do();
				await step.Undo();

				received.Count.ShouldBe(1);
				received[0].ShouldBeNull();
			}

		[Fact]
		public static async Task NullFromCancellableInitializerReachesThePattern()
			{
				var received = new List<string?>();
				CancellableTask<string?> initialize
					= cancel => Task.FromResult<string?>(null);
				var step = Step.NewScoped
					(
						initialize,
						value =>
							{
								received.Add(value);
								return Nothing();
							}
					);

				await step.Do();
				await step.Undo();

				received.Count.ShouldBe(1);
				received[0].ShouldBeNull();
			}

		[Fact]
		public static async Task NonNullInitializerKeepsThePatternNonNull()
			{
				var lengths = new List<int>();
				Func<Task<string>> initialize = () => Task.FromResult("scope");
				var step = Step.NewScoped
					(
						initialize,
						value =>
							{
								lengths.Add(value.Length);
								return Nothing();
							}
					);

				await step.Do();
				await step.Undo();

				lengths.ShouldBe([5]);
			}

		[Fact]
		public static async Task DefaultValueTypeReachesThePattern()
			{
				var received = new List<int>();
				var step = Step.NewScoped
					(
						initialize: () => Task.FromResult(0),
						value =>
							{
								received.Add(value);
								return Nothing();
							}
					);

				await step.Do();
				await step.Undo();

				received.ShouldBe([0]);
			}

		[Fact]
		public static async Task PatternWaitsForInitialization()
			{
				var pending = new TaskCompletionSource<string?>
					(
						TaskCreationOptions.RunContinuationsAsynchronously
					);
				var received = new List<string?>();
				var step = Step.NewScoped
					(
						initialize: () => pending.Task,
						value =>
							{
								received.Add(value);
								return Nothing();
							}
					);

				var perform = step.Do();
				received.ShouldBeEmpty();
				pending.SetResult(null);
				await perform;
				await step.Undo();

				received.Count.ShouldBe(1);
				received[0].ShouldBeNull();
			}

		[Fact]
		public static async Task ExplicitReferenceIsResetAfterUndo()
			{
				var value = Ref<string>.Null;
				var step = Step.NewScoped
					(
						initialize: () => Task.FromResult("scope"),
						value,
						pattern: held => Nothing()
					);

				await step.Do();
				value.Value.ShouldBe("scope");
				await step.Undo();
				value.Value.ShouldBeNull();
			}

		private static IAsyncStep Nothing()
			=> Step.That
				(
					() => Task.CompletedTask,
					() => Task.CompletedTask
				);
	}