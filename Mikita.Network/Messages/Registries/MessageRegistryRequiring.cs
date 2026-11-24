using System;

namespace Mikita.Network.Messages.Registries;

public static class MessageRegistryRequiring
	{
		extension(IMessageRegistry messages)
			{
				/// <summary>
				/// Throws InvalidOperationException unless the type ID is
				/// registered with the expected role.
				/// </summary>
				public void Require(MessageTypeId typeId, MessageRole role)
					{
						if (messages.LabelOf(typeId)?.Role != role)
							throw new InvalidOperationException
								(
									$"Message type {typeId.Value} is not "
									+ $"a registered {role}."
								);
					}
			}
	}