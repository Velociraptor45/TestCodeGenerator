namespace TestCodeGenerator.TestTools;

public static class TestFolder
{
    public static void CreateTemp(Action<string> action)
    {
        var currentPath = Directory.GetCurrentDirectory();
        var folderName = Guid.NewGuid().ToString();

        var tmpFolderPath = Path.Combine(currentPath, "Tests", folderName);
        Directory.CreateDirectory(tmpFolderPath);
        try
        {
            action(tmpFolderPath);
        }
        finally
        {
            Directory.Delete(tmpFolderPath, true);
        }
    }
}