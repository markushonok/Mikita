using Mikita.Network.Messages;
using Mikita.Network.Messages.Matching;
using Mikita.Network.Requests;
using Mikita.Threading;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mikita.Network.Responses.Waiting;

public sealed partial class ResponseWaitMap
	{
		public static IResponseWaitMap New
			=> new ResponseWaitMap
				(
					new Dictionary<RequestId, IResponseWait>()
				);
	}

public sealed partial class ResponseWaitMap
	(
		IDictionary<RequestId, IResponseWait> responses
	)
	: IResponseWaitMap
	{
		public IMatchTask<T> Wait<T>
			(
				RequestId request,
				IMessageMatchMap<T> matches
			)
			{
				var wait = NewWaitWith(matches);

				return responses.TryAdd(request, wait)
					? wait
					: throw OccupiedRequestException(request);
			}

		private static Exception OccupiedRequestException
			(
				RequestId request,
				[CallerArgumentExpression("request")]
				string? expression = null
			)
			=> new ArgumentException
				(
					$"Request `{request}` is already occupied.",
					expression
				);

		private static IResponseWait<T> NewWaitWith<T>
			(
				IMessageMatchMap<T> matches
			)
			=> new ResponseWait<T>
				(
					matches,
					new TaskCompletionSource<Match<T>>
						(
							TaskCreationOptions.RunContinuationsAsynchronously
						)
				);

		public ResponseTake Take
			(
				RequestId request,
				MessageTypeId typeId,
				ReadOnlySpan<byte> bytes
			)
			{
				if (!responses.TryGetValue(request, out var wait))
					return ResponseTake.WaitMiss;

				if (!wait.Take(typeId, bytes))
					return ResponseTake.TypeMiss;

				responses.Remove(request);
				return ResponseTake.Complete;
			}

		public void Fail
			(
				RequestId request,
				Exception exception
			)
			{
				var wait = Remove(request);
				wait?.Fail(exception);
			}

		public void Cancel
			(
				RequestId request,
				CancellationToken cancel
			)
			{
				var wait = Remove(request);
				wait?.Cancel(cancel);
			}

		private IResponseWait? Remove(RequestId request)
			=> responses.Remove(request, out var wait)
				? wait
				: null;
	}