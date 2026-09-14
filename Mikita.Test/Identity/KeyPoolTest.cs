using Mikita.Identity;
using Mikita.Structs.Referring;

namespace Mikita.Test.Identity;

public static class KeyPoolTest
	{
		[Fact]
		public static void SkipsOccupiedKeysAfterOverflow()
			{
				var pool = new KeyPool<int>
					(
						new HashSet<int>([int.MinValue]),
						Ref.To(int.MaxValue),
						key => unchecked(key + 1),
						key => $"Key '{key}' is not acquired."
					);

				pool.Acquire().ShouldBe(int.MinValue + 1);
			}

		[Fact]
		public static void ReleasedKeyCanBeAcquiredAgain()
			{
				var lastKey = Ref.To(int.MaxValue);
				var pool = new KeyPool<int>
					(
						new HashSet<int>(),
						lastKey,
						key => unchecked(key + 1),
						key => $"Key '{key}' is not acquired."
					);
				var released = pool.Acquire();

				pool.Release(released);
				lastKey.Value = int.MaxValue;

				pool.Acquire().ShouldBe(released);
			}

		[Fact]
		public static void UnacquiredKeyUsesProvidedMessage()
			{
				var pool = KeyPool.NewWith
					(
						lastKey: 0,
						nextKey: key => key + 1,
						unacquiredKeyMessage: key => $"Unknown key: {key}."
					);

				var exception = Should.Throw<InvalidOperationException>
					(
						() => pool.Release(7)
					);

				exception.Message.ShouldBe("Unknown key: 7.");
			}
	}