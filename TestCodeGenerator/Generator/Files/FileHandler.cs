using System.Reflection;

namespace TestCodeGenerator.Generator.Files;

public class FileHandler : IFileHandler
{
    public Assembly LoadAssembly(string dllPath)
    {
        var bytes = File.ReadAllBytes(dllPath);
        return Assembly.Load(bytes);
    }
}