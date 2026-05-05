using System;

namespace Mikita.Structs.Buffers.Reading;

public interface IByteReader
	{
		ReadOnlySpan<byte> GetBytes(int length);
	}