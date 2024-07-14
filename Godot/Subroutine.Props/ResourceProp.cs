using Godot;
using Mikita.Subroutine.Props;

namespace Mikita.Godot.Subroutine.Props;

public abstract class ResourceProp<T> : Resource, ReadOnlyProp<T>
	{
		public abstract T Value { get; }
	}