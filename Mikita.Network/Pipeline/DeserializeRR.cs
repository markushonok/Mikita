using Mikita.Network.Peers.Keys;
using Mikita.Network.Requests;
using Mikita.Structs.Buffers.Reading;
using System;

namespace Mikita.Network.Pipeline;

public static class DeserializeRR
	{
		public static IMiddleware With<T>
			(
				ReaderGetWay<ByteReader, T> pattern,
				IMiddlewareRR<T> next
			)
			=> new DeserializeRR<T>(pattern, next);
	}

public sealed class DeserializeRR<T>
	(
		ReaderGetWay<ByteReader, T> pattern,
		IMiddlewareRR<T> next
	)
	: IMiddleware
	{
		public void Handle
			(
				PeerKey peer,
				ReadOnlySpan<byte> bytes
			)
			{
				var reader = new ByteReader(bytes);
				var requestId = reader.GetRequestId();
				var message = pattern(reader);
				next.Handle(peer, requestId, message);
			}
	}