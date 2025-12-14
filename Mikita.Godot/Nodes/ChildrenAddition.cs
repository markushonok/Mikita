using Godot;
using Mikita.Structs.Enumerables;

namespace Mikita.Godot.Nodes;

public static class ChildrenAddition
	{
		public static void AdoptBy
			(
				this Node child,
				Node parent
			)
			=> parent.AddChild(child);

		public static void AddChildren
			(
				this Node parent,
				params IEnumerable<Node> nodes
			)
			=> nodes.ForEach(node => parent.AddChild(node));
	}