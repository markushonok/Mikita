namespace Mikita.Generation.Inference.Types.Trees;

public static class TypeNodeInstancing
	{
		extension(TypeNode)
			{
				public static TypeNode With(string name)
					=> new(name, Arguments: []);
			}
	}