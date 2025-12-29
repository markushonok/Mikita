using Mikita.Generation.Inference.Nodes;
using Mikita.Generation.Inference.Summary.Nodes;

namespace Mikita.Generation.Inference.Summary.Tables;

public interface ISummaryTable
	{
		ISummaryNode SummaryOf(INode node);
	}