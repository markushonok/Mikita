using Mikita.FileSystems.Paths.Formats;
using System;

namespace Mikita.FileSystems.Paths;

public static class MultiElementPath
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
			{
				foreach (var format in PathFormat.All)
					{
						if (format.IsSupport(@string))
							return Path.From(@string, format);
					}

				throw PathFormat.UnknownPathFormatException(@string);
			}
	}