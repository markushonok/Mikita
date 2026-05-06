using System.Threading.Tasks;

namespace Mikita.Steps;

public interface IAsyncStep
	{
		Task Do();

		Task Undo();
	}