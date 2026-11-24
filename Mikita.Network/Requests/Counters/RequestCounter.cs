using Mikita.Structs.Referring;

namespace Mikita.Network.Requests.Counters;

public sealed partial class RequestCounter
	{
		public static IRequestCounter New
			=> new RequestCounter(Ref.To(0));
	}

public sealed partial class RequestCounter
	(
	IRef<int> next
	)
	: IRequestCounter
	{
		public RequestId Next
			=> new(next.Value++);
	}