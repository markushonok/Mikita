using System;

namespace Mikita.Network.Running.Actions;

public delegate void ServerActionLoop
	(
		Func<bool> isRunning
	);