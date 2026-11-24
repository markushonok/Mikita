using Mikita.Structs.Buffers.Reading;
using Mikita.Structs.Buffers.Writing;

namespace Mikita.Network.Requests;

public static class RequestFormat
	{
		extension<TByteReader>(ref TByteReader reader)
			where TByteReader: struct, IByteReader, allows ref struct
			{
				public RequestId GetRequestId()
					=> new(reader.GetInt());
			}

		extension(IByteWriter writer)
			{
				public void Put(RequestId id)
					=> writer.Put(id.Number);
			}
	}