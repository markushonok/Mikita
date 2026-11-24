using System;

namespace Mikita.Network.Requests;

public interface ITimeoutHook: IRequestHook
	{
		void Timeout(TimeSpan duration);
	}