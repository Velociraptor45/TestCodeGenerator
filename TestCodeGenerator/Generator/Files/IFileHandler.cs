using System.Reflection;

namespace TestCodeGenerator.Generator.Files;

public interface IFileHandler
{
    Assembly LoadAssembly(string dllPath);

    bool FileExits(string filePath);
}