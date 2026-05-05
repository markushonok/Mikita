using Mikita.Structs.Referring;

namespace Mikita.Structs.Buffers.Writing;

public static class ReusedByteWriterInstancing
	{
		extension(ReusedByteWriter)
			{
				public static ReusedByteWriter New
					=> new
						(
							version: Ref.To(0),
							position: Ref.To(0),
							buffer: Ref.To<byte[]?>(null)
						);
			}
	}
