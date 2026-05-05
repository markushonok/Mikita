using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace Mikita.Structs.Buffers.Reading;

public static class ReaderGetting
	{
		extension
			<
				TByteReader
			>
			(
				ref TByteReader reader
			)
			where TByteReader: struct, IByteReader, allows ref struct
			{
				public bool GetBool()
					=> reader.GetByte() != 0;

				public byte GetByte()
					=> reader.GetBytes(sizeof(byte))[0];

				public ReadOnlySpan<byte> GetBytes()
					=> reader.GetBytes(reader.GetInt());

				public string GetString()
					{
						var length = reader.GetInt();
						if (length == 0) return string.Empty;
						var bytes = reader.GetBytes(length * sizeof(char));
						var chars = MemoryMarshal.Cast<byte, char>(bytes);
						return new string(chars);
					}

				public short GetShort()
					{
						var bytes = reader.GetBytes(sizeof(short));
						return BinaryPrimitives.ReadInt16LittleEndian(bytes);
					}

				public int GetInt()
					{
						var bytes = reader.GetBytes(sizeof(int));
						return BinaryPrimitives.ReadInt32LittleEndian(bytes);
					}

				public long GetLong()
					{
						var bytes = reader.GetBytes(sizeof(long));
						return BinaryPrimitives.ReadInt64LittleEndian(bytes);
					}
			}
	}
