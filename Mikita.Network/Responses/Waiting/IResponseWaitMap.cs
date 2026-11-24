using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;
using Mikita.Network.Requests;
using Mikita.Threading;
using System;
using System.Threading;

namespace Mikita.Network.Responses.Waiting;

public interface IResponseWaitMap
	{
		IMatchTask<T> Wait<T>
			(
				RequestId request,
				IMessageMatchMap<T> matches
			);

		ResponseTake Take
			(
				RequestId request,
				MessageTypeId typeId,
				ReadOnlySpan<byte> bytes
			);

		void Fail
			(
				RequestId request,
				Exception exception
			);

		void Cancel
			(
				RequestId request,
				CancellationToken cancel
			);
	}