using Mikita.Nulls;
using Mikita.Structs.Referring;
using System;
using System.Buffers;

namespace Mikita.Structs.Buffers.Writing;

public sealed class ReusedByteWriter
	(
		IRef<int> version,
		IRef<int> position,
		IRef<byte[]?> buffer
	)
	: IReusedByteWriter
	{
		public void Put(scoped ReadOnlySpan<byte> bytes)
			{
				EnsureCapacity(position.Value + bytes.Length);
				bytes.CopyTo(buffer.Value.AsSpan(position.Value));
				position.Value += bytes.Length;
			}

		public ReadOnlySpan<byte> Written
			=> buffer.Value is null
				? []
				: buffer.Value.AsSpan(0, position.Value);

		public void Clear()
			{
				version.Value++;
				if (buffer.Value is not null) ReleaseBuffer();
				position.Value = 0;
			}

		private void ReleaseBuffer()
			{
				ArrayPool<byte>.Shared.Return(buffer.Value.NotNull());
				buffer.Value = null;
			}

		public int Version
			=> version.Value;

		private void EnsureCapacity(int requiredCapacity)
			{
				if (buffer.Value is not null && requiredCapacity <= buffer.Value.Length)
					return;

				var newCapacity = NextCapacity(requiredCapacity);
				var newBuffer = ArrayPool<byte>.Shared.Rent(newCapacity);

				if (buffer.Value is not null)
					{
						buffer.Value.AsSpan(0, position.Value).CopyTo(newBuffer);
						ArrayPool<byte>.Shared.Return(buffer.Value);
					}

				buffer.Value = newBuffer;
			}

		private static int NextCapacity(int requiredCapacity)
			{
				var capacity = 256;

				while (capacity < requiredCapacity)
					capacity *= 2;

				return capacity;
			}

	}