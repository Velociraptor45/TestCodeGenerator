namespace TestCodeGenerator.Generator.Files;

public interface IFileHandler
{
    byte[] LoadDll(string dllPath);

    void CreateFile(string path, string content);
}