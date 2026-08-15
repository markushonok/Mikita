namespace Mikita.IO.Folders;

public static class FolderFeature
	{
		extension(IFolder folder)
			{
				public string Name
					=> folder.Path.Elements[^1];
			}
	}