using Mikita.FileSystems.Paths.Formats;
using System;

namespace Mikita.FileSystems.Paths;

partial class Path
	{
		/// <summary>
		/// Represents the current directory path (`./`).
		/// </summary>
		public static Path Current { get; }
			= new([], ascends: 0);

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

				return new Path(elements: [@string], ascends: 0);
			}

		/// <summary>
		/// Creates a path using the provided format to interpret separators.
		/// </summary>
		///
		/// <param name="string">Raw path string.</param>
		/// <param name="format">Format used to split the path.</param>
		///
		/// <returns>Parsed path instance.</returns>
		public static IPath From
			(
				string @string,
				IPathFormat format
			)
			{
				format.Parse(@string, out var path);
				return path;
			}
	}