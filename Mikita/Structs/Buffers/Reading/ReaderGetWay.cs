namespace Mikita.Structs.Buffers.Reading;

public delegate TValue ReaderGetWay
	<
		in TByteReader,
		out TValue
	>
	(
		TByteReader reader
	)
	where TByteReader: IByteReader, allows ref struct;