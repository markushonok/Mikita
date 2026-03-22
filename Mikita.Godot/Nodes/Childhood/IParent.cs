using Godot;

namespace Mikita.Godot.Nodes.Childhood;

public interface IParent
	{
		void Adopt(Node child);

		void Disown(Node child);
	}