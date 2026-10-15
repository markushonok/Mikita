using Mikita.Structs.Referring;

namespace Mikita.Test.Referring;

public static class ProhibitingDuplicatesRefTest
	{
		[Fact]
		public static void LetsTheValueChange()
			{
				var value = Ref.To(0).WithDuplicatesProhibited;

				value.Value = 1;

				value.Value.ShouldBe(1);
			}

		[Fact]
		public static void ThrowsOnARepeatedValue()
			{
				var value = Ref.To(0).WithDuplicatesProhibited;

				Should.Throw<InvalidOperationException>
					(
						() =>
							{
								value.Value = 0;
							}
					);
			}
	}