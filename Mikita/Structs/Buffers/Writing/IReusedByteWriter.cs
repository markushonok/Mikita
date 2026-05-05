namespace Mikita.Structs.Buffers.Writing;

public interface IReusedByteWriter: IByteWriter
	{
		void Clear();

		int Version { get; }
	}