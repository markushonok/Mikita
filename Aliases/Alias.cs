using System.Runtime.CompilerServices;

namespace Mikita.Aliases;

public interface Alias<out TOrigin, out TNew>
	where TNew : TOrigin
 	{
		static TNew AliasOf(TOrigin origin)
			=> Alias<TOrigin, TNew>.Of(origin);
		
		static TNew Of(TOrigin origin)
			=> Alias<TOrigin, TNew>.Of(ref origin);

		static TNew Of(ref TOrigin origin)
			=> Unsafe.As<TOrigin, TNew>(ref origin);
	}