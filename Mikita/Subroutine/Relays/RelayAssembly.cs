using System.Reflection;
using System.Reflection.Emit;

namespace Mikita.Subroutine.Relays;

public static class RelayAssembly
	{
		public static readonly AssemblyBuilder Builder
			= AssemblyBuilder.DefineDynamicAssembly
				(
					new AssemblyName(Relay.Namespace),
					AssemblyBuilderAccess.RunAndCollect
				);
	}