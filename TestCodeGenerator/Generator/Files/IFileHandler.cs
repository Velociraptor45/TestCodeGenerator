using System.Reflection;

namespace TestCodeGenerator.Generator.Files;

public interface IFileHandler
{
    Assembly LoadAssembly(string dllPath);

    void CreateFile(string path, string content);
}