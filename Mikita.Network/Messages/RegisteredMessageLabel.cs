namespace Mikita.Network.Messages;

public sealed class RegisteredMessageLabel
	(
		string typeName,
		MessageRole role,
		MessageTypeId typeId
	)
	: IRegisteredMessageLabel
	{
		public string TypeName
			=> typeName;

		public MessageRole Role
			=> role;

		public MessageTypeId TypeId
			=> typeId;
	}