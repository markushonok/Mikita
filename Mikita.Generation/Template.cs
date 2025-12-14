using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Mikita.Generation;

public static class Template
	{
		public static string WithUniqueName(string name)
			{
				var path = Assembly
					.GetManifestResourceNames()
					.Single(n => n.EndsWith($".{name}", StringComparison.Ordinal));

				return Template.AtPath(path);
			}

		public static string AtPath(string path)
			{
				using var stream = Assembly.GetManifestResourceStream(path);
				using var reader = new StreamReader(stream!);
				return reader.ReadToEnd();
			}

		private static Assembly Assembly
			=> typeof(Template).Assembly;
	}