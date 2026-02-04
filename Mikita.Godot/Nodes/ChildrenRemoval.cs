using Godot;

namespace Mikita.Godot.Nodes;

public static class ChildrenRemoval
	{
		extension(Node parent)
			{
				public void ReleaseChildren()
					{
						foreach (var child in parent.GetChildren())
							parent.RemoveChild(child);
					}
			}
	}