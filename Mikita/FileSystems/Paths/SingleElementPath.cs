using System;

namespace Mikita.FileSystems.Paths;

public static class SingleElementPath
	{
		/// <summary>
		/// Creates a path using the first supported format from the list.
		/// </summary>
		///
		/// <param name="string">
		/// Raw path string containing '/' or '\\' as separators.
		/// </param>
		///
		/// <returns>Path parsed with matching format.</returns>
		///
		/// <exception cref="InvalidOperationException">
		/// Thrown if no matching format is found.
		/// </exception>
		public static IPath From(string @string)
			=> new Path
				(
					elements: [@string],
					ascends: 0
				);
	}