using Godot;

namespace Mikita.Godot.Nodes.Childhood;

public sealed class Parent
	(
		Func<Node> parent
	)
	: IParent
	{
		public void Adopt(Node child)
			=> parent().AddChild(child);

		public void Disown(Node child)
			=> parent().RemoveChild(child);
	}