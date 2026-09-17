namespace Mikita.Steps;

/// <summary>
/// A reversible operation that can be performed and then rolled back.
/// </summary>
/// <remarks>
/// Calls must alternate, starting with <see cref="Do"/>. Calling
/// <see cref="Undo"/> before <see cref="Do"/>, or <see cref="Do"/> twice
/// in a row, is not supported.
/// </remarks>
public interface IStep
	{
		/// <summary>
		/// Performs the operation.
		/// </summary>
		void Do();

		/// <summary>
		/// Reverses the effect of the most recent <see cref="Do"/>.
		/// </summary>
		void Undo();
	}