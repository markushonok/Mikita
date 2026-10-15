using Mikita.Structs.Referring;

namespace Mikita.Test.Referring;

public static class IgnoringDuplicatesRefTest
	{
		[Fact]
		public static void LetsTheValueChange()
			{
				var value = Ref.To(0).WithDuplicatesIgnored;

				value.Value = 1;

				value.Value.ShouldBe(1);
			}

		[Fact]
		public static void DoesNotReachTheSourceOnARepeatedValue()
			{
				var changes = 0;
				var value = Ref
					.To(0)
					.WithCallback(() => changes++)
					.WithDuplicatesIgnored;

				value.Value = 0;
				value.Value = 0;
				value.Value = 1;

				changes.ShouldBe(1);
			}
	}