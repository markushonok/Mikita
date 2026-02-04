namespace Mikita.States.Orders;

public interface IStateOrderItem<out T>
	{
		T Key { get; }

		bool IsActive { get; }
	}