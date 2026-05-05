using System;

namespace Mikita.Structs.Buffers.Writing;

public interface IByteWriter
	{
		void Put(scoped ReadOnlySpan<byte> bytes);

		ReadOnlySpan<byte> Written { get; }
	}