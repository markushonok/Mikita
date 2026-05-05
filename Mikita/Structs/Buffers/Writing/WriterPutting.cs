using System;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Mikita.Structs.Buffers.Writing;

public static class WriterPutting
	{
		extension
			<
				TByteWriter
			>
			(
				TByteWriter writer
			)
			where TByteWriter: IByteWriter, allows ref struct
			{
				public void Put(bool value)
					=> writer.Put(value ? (byte) 1 : (byte) 0);

				public void Put(byte value)
					=> writer.Put([value]);

				public void Put(string value)
					{
						writer.Put(value.Length);
						writer.Put(value.AsSpan());
					}

				public void Put(ReadOnlySpan<char> values)
					=> writer.Put(MemoryMarshal.AsBytes(values));

				public void Put<TInteger>(TInteger value)
					where TInteger: struct, IBinaryInteger<TInteger>
					{
						Span<byte> buf = stackalloc byte[16];
						if (!value.TryWriteLittleEndian(buf, out var written))
							throw new InvalidOperationException();
						writer.Put(buf[..written]);
					}

				public void Put<TValue>
					(
						WriterPutWay<TByteWriter, TValue> way,
						TValue value
					)
					=> way(writer, value);
			}
	}
