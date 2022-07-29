namespace Generator.Files;

public class FileHandler : IFileHandler
{
    public byte[] LoadDll(string dllPath)
    {
        return File.ReadAllBytes(dllPath);
    }

    public void CreateFile(string path, string content)
    {
        if (File.Exists(path))
            File.Delete(path);

        File.WriteAllText(path, content);
    }
}