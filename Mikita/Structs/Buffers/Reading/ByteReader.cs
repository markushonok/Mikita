using System;

namespace Mikita.Structs.Buffers.Reading;

public ref struct ByteReader: IByteReader
	{
		public ByteReader(ReadOnlySpan<byte> bytes)
			=> this.bytes = bytes;

		public ReadOnlySpan<byte> GetBytes(int length)
			{
				var part = bytes[..length];
				bytes = bytes[length..];
				return part;
			}

		private ReadOnlySpan<byte> bytes;
	}