using Mikita.Observation;

namespace Mikita.Generation.Sample;

public static class StaticClass
	{
		public static void MethodA()
			{
				MethodB<IInterfaceA>();
			}

		public static void MethodB<T>()
			{
				IInterfaceB instance = new InstanceClass<T>();
				instance.GenericMethod<IEnumerable<T>>();
			}
	}

public interface IInterfaceA;

public interface IInterfaceB
	{
		void GenericMethod<T>();
	}

public sealed class InstanceClass<T1>: IInterfaceB
	{
		public void GenericMethod<T2>()
			{
				Broadcast.To<T1>([]);
				Broadcast.To<T2>([]);
			}
	}