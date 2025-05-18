using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task = Microsoft.Build.Utilities.Task;

namespace MSBuild.Targets;

/// <summary>
/// MSBuild task for automatic file nesting in Visual Studio projects.
/// Detects parent-child relationships by finding files that match pattern: ParentName.AnySuffix.cs
/// </summary>
public class AutoFileNestTask : Task
	{
		/// <summary>
		/// Source files to be processed for nesting.
		/// </summary>
		[Required]
		public required ITaskItem[] Files { get; set; }

		/// <summary>
		/// Files with updated nesting metadata.
		/// </summary>
		[Output]
		public required ITaskItem[] UpdatedFiles { get; set; }

		/// <summary>
		/// Automatically process files and apply nesting metadata.
		/// </summary>
		public override bool Execute()
			{
				var processedFiles = new List<ITaskItem>();
				var fileGroups = FilesGroupedByDirectory();

				foreach (var files in fileGroups.Values)
					processedFiles.AddRange(Handled(files));

				UpdatedFiles = processedFiles.ToArray();
				return true;
			}

		/// <summary>
		/// Group files by their containing directory to process them in context.
		/// </summary>
		private Dictionary<string, List<FileInfo>> FilesGroupedByDirectory()
			{
				var result = new Dictionary<string, List<FileInfo>>();

				foreach (var file in Files)
					{
						var filePath = file.ItemSpec;

						if (IsImportedFile(file))
							continue;

						var directory = Path.GetDirectoryName(filePath)
														?? throw new FileNotFoundException("File doesn't have path");

						var fileName = Path.GetFileName(filePath);

						if (!result.ContainsKey(directory))
							result[directory] = [];

						result[directory].Add(new FileInfo(file, fileName));
					}

				return result;
			}

		/// <summary>
		/// Проверяет, является ли файл импортированным из SDK или других внешних источников
		/// </summary>
		private bool IsImportedFile(ITaskItem file)
			{
				// Проверяем различные метаданные, которые могут указывать на импортированный файл
				var importedFromSdk = file.GetMetadata("ImportedFromSdk");
				if (!string.IsNullOrEmpty(importedFromSdk)
						&& importedFromSdk.Equals(
							"true",
							StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}

				// Проверяем, содержит ли путь к файлу ссылку на SDK или другие системные пути
				var filePath = file.ItemSpec;
				if (filePath.Contains(@"\Sdks\") ||
						filePath.Contains(@"\dotnet\") ||
						filePath.Contains(@"\Microsoft.NET.Sdk\"))
					return true;

				// Проверяем, есть ли метаданные о том, что файл был импортирован из .props или .targets
				var importedFrom = file.GetMetadata("ImportedFromTargetsFile");
				if (!string.IsNullOrEmpty(importedFrom))
					return true;

				return false;
			}

		/// <summary>
		/// Process all files in a directory and detect nesting relationships.
		/// </summary>
		private List<ITaskItem> Handled(ICollection<FileInfo> files)
			{
				var processedFiles = new List<ITaskItem>();

				// Подготовка всех файлов и их метаданных
				foreach (var file in files)
					{
						var taskItem = new TaskItem(file.OriginalItem);
						file.OriginalItem.CopyMetadataTo(taskItem);
						processedFiles.Add(taskItem);
						file.TaskItem = taskItem;
					}

				var sourceFiles = files
					.Where(f => f.FileName.EndsWith(".cs"))
					.ToList();

				// Сортируем файлы по длине имени (короткие имена первыми - потенциальные родители)
				sourceFiles.Sort((a, b) => a.FileName.Length.CompareTo(b.FileName.Length));

				// Для каждого файла проверяем, может ли он быть родителем для других файлов
				foreach (var sourceFile in sourceFiles)
					{
						var parentName = Path.GetFileNameWithoutExtension(sourceFile.FileName);

						// Проверяем каждый файл на возможность быть дочерним
						foreach (var potentialChild in sourceFiles)
							{
								// Пропускаем тот же самый файл
								if (sourceFile == potentialChild)
									continue;

								// Пропускаем если файл уже вложен
								if (!string.IsNullOrEmpty(potentialChild.TaskItem.GetMetadata("DependentUpon")))
									continue;

								var childName = Path.GetFileNameWithoutExtension(potentialChild.FileName);

								// Простой шаблон: Дочерний файл начинается с имени родителя + точка (Родитель.ЛюбойСуффикс)
								if (childName.StartsWith(parentName + "."))
									{
										// Применяем вложение
										potentialChild.TaskItem.SetMetadata("DependentUpon", sourceFile.FileName);

										Log.LogMessage(
											MessageImportance.Normal,
											$"Nesting applied: {potentialChild.FileName} → {sourceFile.FileName}");
									}
							}
					}
				return processedFiles;
			}

		/// <summary>
		/// Helper class to store file information during processing.
		/// </summary>
		private class FileInfo
			{
				public ITaskItem OriginalItem { get; }
				public string FileName { get; }
				public ITaskItem TaskItem { get; set; }

				public FileInfo(ITaskItem item, string fileName)
					{
						OriginalItem = item;
						FileName = fileName;
					}
			}
	}