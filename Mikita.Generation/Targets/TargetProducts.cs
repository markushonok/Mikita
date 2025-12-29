using System;

namespace Mikita.Generation.Targets;

[Flags]
public enum TargetProducts
	{
		None,
		Broadcast,
		Live,
		Log
	}