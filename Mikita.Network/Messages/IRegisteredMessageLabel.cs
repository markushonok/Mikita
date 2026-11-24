namespace Mikita.Network.Messages;

/// <summary>
/// An immutable message description with an ID assigned by one registry.
/// </summary>
public interface IRegisteredMessageLabel: IMessageLabel
	{
		MessageTypeId TypeId { get; }
	}