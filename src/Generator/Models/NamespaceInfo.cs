namespace TestCodeGenerator.Generator.Models;
public class NamespaceInfo
{
    public NamespaceInfo(Type type, string outputRootNamespace)
    {
        var rootNamespaceParts = outputRootNamespace.Split('.');
        var typeNamespaceParts = type.Namespace!.Split('.');

        var maxLength = Math.Max(rootNamespaceParts.Length, typeNamespaceParts.Length);

        for (int i = 0; i < maxLength; i++)
        {
            if (typeNamespaceParts.Length <= i)
            {
                BuilderClassNamespace = outputRootNamespace;
                InAssemblyPath = string.Empty;
                return;
            }

            if (rootNamespaceParts.Length <= i)
            {
                BuilderClassNamespace = type.Namespace!;
                InAssemblyPath = string.Join('.', typeNamespaceParts[i..]);
                return;
            }

            if (rootNamespaceParts[i] == typeNamespaceParts[i])
                continue;

            InAssemblyPath = string.Join('.', typeNamespaceParts[i..]);
            BuilderClassNamespace = $"{string.Join('.', rootNamespaceParts)}.{InAssemblyPath}";
            return;
        }

        BuilderClassNamespace = outputRootNamespace;
        InAssemblyPath = string.Empty;
    }

    public string BuilderClassNamespace { get; }
    public string InAssemblyPath { get; }
}
