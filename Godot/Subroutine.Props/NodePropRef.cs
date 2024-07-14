using Godot;

namespace Mikita.Godot.Subroutine.Props;

public class NodePropRef<T> : ResourceProp<T>
	{
		public override T Value
			=> throw new NotImplementedException();
		
		[Export]
		public Node? Node { get; set; }
		
		[Export]
		public string? PropertyPath { get; set; }
	}