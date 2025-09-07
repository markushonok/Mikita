using System.Reflection.Emit;

namespace Mikita.Evaluation.Liveness;

public static class LiveModule
	{
		public static readonly ModuleBuilder Builder
			= LiveAssembly
				.Builder
				.DefineDynamicModule(Live.Namespace);
	}