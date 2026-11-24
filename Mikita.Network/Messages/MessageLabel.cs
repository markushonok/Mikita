namespace Mikita.Network.Messages;

public readonly record struct MessageLabel
	(
		string TypeName,
		MessageRole Role
	)
	: IMessageLabel;