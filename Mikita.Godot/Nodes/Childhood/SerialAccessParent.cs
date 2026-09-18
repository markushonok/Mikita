using Godot;
using System.Threading;

namespace Mikita.Godot.Nodes.Childhood;

/// <summary>
/// Serializes calls to another parent through a shared access lock.
/// </summary>
/// <param name="source">Parent receiving serialized calls.</param>
/// <param name="access">Serializes calls that share protected state.</param>
public sealed class SerialAccessParent
	(
		IParent source,
		Lock access
	)
	: IParent
	{
		public void Adopt(Node child)
			{
				lock (access) source.Adopt(child);
			}

		public void Disown(Node child)
			{
				lock (access) source.Disown(child);
			}
	}