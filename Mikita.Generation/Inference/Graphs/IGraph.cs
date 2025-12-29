using Mikita.Generation.Inference.Edges;

namespace Mikita.Generation.Inference.Graphs;

public interface IGraph: IReadOnlyGraph
	{
		void Add(IEdge edge);
	}