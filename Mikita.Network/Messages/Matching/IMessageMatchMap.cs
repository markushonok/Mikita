namespace Mikita.Network.Messages.Matching;

public interface IMessageMatchMap<in T>
	{
		IMessageMatch<T>? this[MessageTypeId typeId] { get; }
	}