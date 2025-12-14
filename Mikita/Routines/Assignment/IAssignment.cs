namespace Mikita.Routines.Assignment;

public interface IAssignment<in T>
	{
		void SetTo(T value);
	}