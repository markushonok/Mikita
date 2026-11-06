using System;

namespace Mikita.Threading;

/// <summary>
/// Receives a failure through a match outcome. The string overload reports a
/// message from the operation; the action overload lets the caller supply the
/// exact exception-raising behavior without throwing before the hook is
/// returned.
/// </summary>
public interface IErrorHook
	{
		void Error(string message);

		void Error(Action @throw)
			=> @throw();
	}