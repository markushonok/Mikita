using Mikita.Threading;
using System;

namespace Mikita.Network.Messages.Matching;

public interface IMessageMatch<in T>
	{
		MessageTypeId Id { get; }

		Match<T> Of(ReadOnlySpan<byte> bytes);
	}