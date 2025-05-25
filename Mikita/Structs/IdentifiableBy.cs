namespace Mikita.Structs;

public interface IdentifiableBy<out T>
	{
		T Id { get; }
	}