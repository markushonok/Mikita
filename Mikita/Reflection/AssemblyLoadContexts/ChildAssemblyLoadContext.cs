using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Mikita.Reflection.AssemblyLoadContexts;

public sealed class ChildAssemblyLoadContext
	(
		AssemblyLoadContext parent,
		string? name,
		bool isCollectible
	)
	: AssemblyLoadContext
		(
			name,
			isCollectible
		)
	{
		protected override Assembly? Load
			(
				AssemblyName name
			)
			=> base.Load(name) ?? ParentAssembly(name);

		private Assembly? ParentAssembly(AssemblyName name)
			=> parent.LoadFromAssemblyName(name);
	}