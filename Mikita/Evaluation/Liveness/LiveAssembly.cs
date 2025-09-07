using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Evaluation.Liveness;

public static class LiveAssembly
	{
		public static readonly AssemblyBuilder Builder
			= AssemblyBuilder.DefineDynamicAssembly
				(
					new AssemblyName(Live.Namespace),
					AssemblyBuilderAccess.RunAndCollect
				);
	}