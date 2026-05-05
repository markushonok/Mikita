namespace Mikita.Structs.Buffers.Writing;

public static class ByteWriterPoolInstancing
	{
		extension(ByteWriterPool)
			{
				public static IByteWriterPool New
					=> new ByteWriterPool(ReusedByteWriter.New);
			}
	}
