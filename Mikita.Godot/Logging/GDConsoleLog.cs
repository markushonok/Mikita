#pragma warning disable CA2255

using Godot;
using Mikita.Logging;
using System.Runtime.CompilerServices;

namespace Mikita.Godot.Logging;

public sealed class GDConsoleLog: ILog
	{
		public void Write(string @string)
			=> GD.Print(@string);

		[ModuleInitializer]
		internal static void InitDebug()
			=> DebugLog.Add(new GDConsoleLog());
	}