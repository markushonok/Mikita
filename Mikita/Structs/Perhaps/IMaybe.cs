namespace Mikita.Structs.Perhaps;

public interface IMaybe<out T>
	{
		T Current { get; }

		bool IsSome { get; }
	}