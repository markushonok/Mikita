using System;

namespace Mikita.Structs.Buffers.Writing;

public readonly struct RentedByteWriter
	(
		IReusedByteWriter writer,
		int version
	)
	: IRentedByteWriter
	{
		public void Put(scoped ReadOnlySpan<byte> bytes)
			{
				AssertVersion();
				writer.Put(bytes);
			}

		public ReadOnlySpan<byte> Written
			{
				get
					{
						AssertVersion();
						return writer.Written;
					}
			}

		public void Dispose()
			{
				AssertVersion();
				writer.Clear();
			}

		private void AssertVersion()
			=> ObjectDisposedException.ThrowIf
				(
					writer.Version != version,
					typeof(RentedByteWriter)
				);
	}
