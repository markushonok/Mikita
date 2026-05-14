namespace Mikita.FileSystems.Folders;

public static class FolderFeature
	{
		extension(IFolder folder)
			{
				public string Name
					=> folder.Path.Elements[^1];
			}
	}