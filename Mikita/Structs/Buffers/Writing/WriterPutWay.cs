namespace Mikita.Structs.Buffers.Writing;

public delegate void WriterPutWay
	<
		in TByteWriter,
		in TValue
	>
	(
		TByteWriter writer,
		TValue value
	)
	where TByteWriter: IByteWriter, allows ref struct;