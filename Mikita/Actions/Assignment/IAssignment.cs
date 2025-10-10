namespace Mikita.Actions.Assignment;

public interface IAssignment<in T>
	{
		void SetTo(T value);
	}