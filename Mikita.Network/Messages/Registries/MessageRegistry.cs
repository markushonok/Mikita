using System;
using System.Collections.Generic;

namespace Mikita.Network.Messages.Registries;

public sealed partial class MessageRegistry
	{
		public static IMessageRegistry New
			=> new MessageRegistry
				(
					new Dictionary<string, IRegisteredMessageLabel>
						(
							StringComparer.Ordinal
						),
					new List<IRegisteredMessageLabel>()
				);
	}

/// <summary>
/// Uses externally owned name and ordered label collections. Keep them
/// consistent and register through Register. Names must use ordinal comparison.
/// </summary>
public sealed partial class MessageRegistry
	(
		IDictionary<string, IRegisteredMessageLabel> labels,
		IList<IRegisteredMessageLabel> orderedLabels
	)
	: IMessageRegistry
	{
		public void Register(IEnumerable<IMessageLabel> labels)
			{
				foreach (var label in labels)
					Register(label);
			}

		public IRegisteredMessageLabel Register(IMessageLabel label)
			{
				if (labels.TryGetValue(label.TypeName, out var existing))
					{
						if (existing.Role != label.Role)
							throw new InvalidOperationException
								(
									$"Message '{label.TypeName}' already has role "
									+ $"{existing.Role}."
								);
						return existing;
					}

				if (orderedLabels.Count > short.MaxValue)
					throw new InvalidOperationException
						(
							"Message type IDs are exhausted."
						);

				var registered = new RegisteredMessageLabel
					(
						label.TypeName,
						label.Role,
						new MessageTypeId((short) orderedLabels.Count)
					);
				labels.Add(label.TypeName, registered);
				orderedLabels.Add(registered);
				return registered;
			}

		public IRegisteredMessageLabel LabelOf(string typeName)
			=> labels[typeName];

		public IRegisteredMessageLabel? LabelOf(MessageTypeId typeId)
			=> typeId.Value >= 0 && typeId.Value < orderedLabels.Count
				? orderedLabels[typeId.Value]
				: null;
	}