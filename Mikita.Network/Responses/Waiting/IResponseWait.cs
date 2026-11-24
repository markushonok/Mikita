using Mikita.Network.Messages;
using Mikita.Threading;
using System;
using System.Threading;

namespace Mikita.Network.Responses.Waiting;

public interface IResponseWait
	{
		bool Take
			(
				MessageTypeId typeId,
				ReadOnlySpan<byte> bytes
			);

		void Cancel(CancellationToken cancel);

		void Fail(Exception exception);
	}

public interface IResponseWait<in T>: IResponseWait, IMatchTask<T>;