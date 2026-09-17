using System.Threading.Tasks;

namespace Mikita.Steps;

/// <summary>
/// A reversible operation whose <see cref="Do"/> and <see cref="Undo"/>
/// complete asynchronously.
/// </summary>
/// <remarks>
/// Calls must alternate, starting with <see cref="Do"/>. Calling
/// <see cref="Undo"/> before <see cref="Do"/>, or <see cref="Do"/> twice
/// in a row, is not supported.
/// </remarks>
public interface IAsyncStep
	{
		/// <summary>
		/// Performs the operation.
		/// </summary>
		Task Do();

		/// <summary>
		/// Reverses the effect of the most recent <see cref="Do"/>.
		/// </summary>
		Task Undo();
	}