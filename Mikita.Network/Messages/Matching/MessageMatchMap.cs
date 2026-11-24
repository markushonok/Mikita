using System.Collections.Frozen;
using System.Collections.Generic;

namespace Mikita.Network.Messages.Matching;

public static class MessageMatchMap
	{
		public static IMessageMatchMap<T> From<T>
			(
				IMessageMatch<T> match
			)
			=> From([match]);

		public static IMessageMatchMap<T> From<T>
			(
				IEnumerable<IMessageMatch<T>> matches
			)
			=> new MessageMatchMap<T>
				(
					matches.ToFrozenDictionary(match => match.Id)
				);
	}

public sealed class MessageMatchMap<T>
	(
		IReadOnlyDictionary<MessageTypeId, IMessageMatch<T>> matches
	)
	: IMessageMatchMap<T>
	{
		public IMessageMatch<T>? this[MessageTypeId typeId]
			=> matches.GetValueOrDefault(typeId);
	}