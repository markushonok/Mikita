using Godot;

namespace Mikita.Godot.Nodes.Childhood;

public static class ParentInstancing
	{
		extension(Node node)
			{
				public Parent AsParent
					=> new(() => node);
			}
	}