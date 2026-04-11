using Godot;

namespace Mikita.Godot.Nodes.Childhood;

public static class ParentInstancing
	{
		extension(Node node)
			{
				public IParent AsParent
					=> new Parent(() => node);
			}
	}