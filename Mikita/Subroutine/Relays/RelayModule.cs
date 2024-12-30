using System.Reflection.Emit;

namespace Mikita.Subroutine.Relays;

public static class RelayModule
	{
		public static readonly ModuleBuilder Builder
			= RelayAssembly
				.Builder
				.DefineDynamicModule(Relay.Namespace);
	}