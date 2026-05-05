namespace Mikita.Structs.Unions;

public interface IUnion<in T>
	{
		void Switch(T handle);
	}