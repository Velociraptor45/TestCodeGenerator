using System.Reflection;

namespace TestCodeGenerator.Generator.Files;

public class FileHandler : IFileHandler
{
    public Assembly LoadAssembly(string dllPath)
    {
        return Assembly.LoadFrom(dllPath);
    }

    public bool FileExits(string filePath)
    {
        return File.Exists(filePath);
    }
}