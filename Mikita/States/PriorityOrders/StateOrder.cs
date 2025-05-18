namespace Mikita.States.PriorityOrders;

public interface StateOrder<out T>
	{
		T Current { get; }
	}