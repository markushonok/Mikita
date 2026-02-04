using Godot;
using Mikita.Godot.Threading;
using Mikita.Structs.Enumerables;
using Mikita.Threading;

namespace Mikita.Godot.Nodes;

public static class ChildrenAddition
	{
		extension(Node parent)
			{
				public void Adopt
					(
						IEnumerable<Node> children
					)
					=> children.ForEach(child => parent.AddChild(child));

				public Task AdoptAsync<T>
					(
						Task<T> child
					)
					where T: Node
					=> parent.AdoptAsync(child.AsITask);

				public Task AdoptAsync
					(
						ITask<Node> child
					)
					=> parent.Defer(async x => x.AddChild(await child));

				public void Adopt
					(
						Node child
					)
					=> parent.AddChild(child);
			}

		extension(Node child)
			{
				public void AdoptBy
					(
						Node parent
					)
					=> parent.AddChild(child);
			}
	}