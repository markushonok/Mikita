namespace Mikita.Structs.Buffers.Writing;

public sealed class ByteWriterPool
	(
		IReusedByteWriter writer
	)
	: IByteWriterPool
	{
		public RentedByteWriter Rent()
			=> new(writer, writer.Version);
	}
