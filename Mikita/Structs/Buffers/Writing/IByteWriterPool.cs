namespace Mikita.Structs.Buffers.Writing;

public interface IByteWriterPool
	{
		RentedByteWriter Rent();
	}