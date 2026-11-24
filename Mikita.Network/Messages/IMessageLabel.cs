namespace Mikita.Network.Messages;

/// <summary>
/// Describes a message by its case-sensitive type name and role.
/// </summary>
public interface IMessageLabel
	{
		string TypeName { get; }

		MessageRole Role { get; }
	}