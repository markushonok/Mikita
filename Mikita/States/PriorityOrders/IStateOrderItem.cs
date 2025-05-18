namespace Mikita.States.PriorityOrders;

public interface IStateOrderItem<out T>
	{
		T Key { get; }

		bool IsActive { get; }
	}