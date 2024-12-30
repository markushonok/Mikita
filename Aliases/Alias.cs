namespace Mikita.Aliases;

public abstract class Alias<T>(T fullTyped)
	{
		public T FullTyped { get; } = fullTyped;
		
		public static implicit operator T(Alias<T> alias) 
			=> alias.FullTyped;
	}