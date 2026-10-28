using Mikita.Evaluation.Liveness;
using Mikita.IO.Paths;

namespace Mikita.Test.Evaluation;

public static class LiveRelayTest
	{
		[Fact]
		public static void LazyPathRelayIsRegistered()
			=> LiveRelay<IPath>.Factory.ShouldNotBeNull();

		[Fact]
		public static void GeneratedRelayIsRegistered()
			=> LiveRelay<ILiveSubject>.Factory.ShouldNotBeNull();

		[Fact]
		public static void GeneratedRelayComesFromTheDeclaringAssembly()
			{
				var subject = new Subject();
				var live = Live.ResultOf<ILiveSubject>(() => subject);

				live.GetType().Assembly.ShouldBe(typeof(ILiveSubject).Assembly);
			}

		[Fact]
		public static void GeneratedRelayReadsTheCurrentValue()
			{
				var subject = new Subject { Value = 1 };
				var live = Live.ResultOf<ILiveSubject>(() => subject);

				subject.Value = 2;

				live.Value.ShouldBe(2);
			}

		[Fact]
		public static void GeneratedRelayWritesThrough()
			{
				var subject = new Subject();
				var live = Live.ResultOf<ILiveSubject>(() => subject);

				live.Value = 5;

				subject.Value.ShouldBe(5);
			}

		[Fact]
		public static void GeneratedRelayForwardsMethodCall()
			{
				var subject = new Subject { Value = 3 };
				var live = Live.ResultOf<ILiveSubject>(() => subject);

				live.Describe("!").ShouldBe("3!");
			}


		[Fact]
		public static void UndeclaredTypeThrows()
			{
				var subject = new Unrelated();

				var thrown = Should
					.Throw<NotSupportedException>
						(
							() => Live.ResultOf<IUnrelated>(() => subject)
						);

				thrown.Message.ShouldContain("Implementations.toml");
			}

		[Fact]
		public static void GeneratedRelayForwardsObjectMembers()
			{
				var subject = new Subject { Value = 7 };
				var live = Live.ResultOf<ILiveSubject>(() => subject);

				live.ToString().ShouldBe(subject.ToString());
				live.GetHashCode().ShouldBe(subject.GetHashCode());
				live.Equals(subject).ShouldBeTrue();
			}

		private sealed class Subject
			: ILiveSubject
			{
				public int Value { get; set; }

				public string Describe(string suffix)
					=> Value + suffix;
			}

		public interface ILiveSubject
			{
				int Value { get; set; }

				string Describe(string suffix);
			}

		private sealed class Unrelated: IUnrelated;

		private interface IUnrelated;
	}