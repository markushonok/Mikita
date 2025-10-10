using Godot;
using Mikita.Structs.Enumerables;

namespace Mikita.Godot.Nodes;

public static class ChildrenAddition
	{
		public static void AddChildren
			(
				this Node parent,
				params IEnumerable<Node> nodes
			)
			=> nodes.ForEach(node => parent.AddChild(node));
	}