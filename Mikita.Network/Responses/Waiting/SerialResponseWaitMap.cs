using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;
using Mikita.Network.Requests;
using Mikita.Threading;
using System;
using System.Threading;

namespace Mikita.Network.Responses.Waiting;

/// <summary>
/// Serializes access to another <see cref="IResponseWaitMap"/>.
/// </summary>
/// <param name="source">Map receiving serialized calls.</param>
/// <param name="access">Serializes public API calls.</param>
public sealed class SerialResponseWaitMap
	(
		IResponseWaitMap source,
		Lock access
	)
	: IResponseWaitMap
	{
		public IMatchTask<T> Wait<T>
			(
				RequestId request,
				IMessageMatchMap<T> matches
			)
			{
				lock (access) return source.Wait(request, matches);
			}

		public ResponseTake Take
			(
				RequestId request,
				MessageTypeId typeId,
				ReadOnlySpan<byte> bytes
			)
			{
				lock (access) return source.Take(request, typeId, bytes);
			}

		public void Fail
			(
				RequestId request,
				Exception exception
			)
			{
				lock (access) source.Fail(request, exception);
			}

		public void Cancel
			(
				RequestId request,
				CancellationToken cancel
			)
			{
				lock (access) source.Cancel(request, cancel);
			}
	}