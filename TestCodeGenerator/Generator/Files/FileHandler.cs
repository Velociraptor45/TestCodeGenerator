using System.Reflection;

namespace TestCodeGenerator.Generator.Files;

public class FileHandler : IFileHandler
{
    public Assembly LoadAssembly(string dllPath)
    {
        var bytes = File.ReadAllBytes(dllPath);
        return Assembly.Load(bytes);
    }

    public void CreateFile(string path, string content)
    {
        if (File.Exists(path))
            File.Delete(path);

        File.WriteAllText(path, content);
    }
}