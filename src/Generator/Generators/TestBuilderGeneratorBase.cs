using RefleCS;
using RefleCS.Nodes;
using System.Reflection;
using System.Text.RegularExpressions;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Generators;

public abstract class TestBuilderGeneratorBase
{
    private readonly Regex _csClassNameRegex = new(@"^[A-Za-z_][A-Za-z0-9_]*$");

    private readonly IFileHandler _fileHandler;
    private readonly ICsFileHandler _csFileHandler;
    protected readonly BuilderConfiguration Config;

    protected TestBuilderGeneratorBase(IFileHandler fileHandler, ICsFileHandler csFileHandler, BuilderConfiguration config)
    {
        _fileHandler = fileHandler;
        _csFileHandler = csFileHandler;
        Config = config;
    }

    public void Generate(IEnumerable<string> typeNames)
    {
        var assembly = _fileHandler.LoadAssembly(Config.DllPath);

        var typeNamesList = typeNames.ToList();
        foreach (var typeName in typeNamesList)
        {
            Console.WriteLine();
            var type = RetrieveTypeFromAssembly(typeName, assembly);
            if (type is null)
                continue;

            Console.WriteLine($"Starting code generation for {typeName}");

            var builderClassName = GenerateBuilderClassName(type);

            var namespaceInfo = new NamespaceInfo(type, Config.OutputAssemblyRootNamespace);
            var builderFilePath = Config.MatchFolderToNamespace
                ? Path.Combine(Config.OutputFolder, namespaceInfo.InAssemblyPath.Replace('.', Path.DirectorySeparatorChar), $"{builderClassName}.cs")
                : Path.Combine(Config.OutputFolder, $"{builderClassName}.cs");

            var file = _fileHandler.FileExits(builderFilePath)
                ? _csFileHandler.FromFile(builderFilePath)
                    ?? new CsFile([], new Namespace(namespaceInfo.BuilderClassNamespace))
                : new CsFile([], new Namespace(namespaceInfo.BuilderClassNamespace));

            var success = UpdateFile(file, type, builderClassName);
            if (!success)
            {
                Console.WriteLine($"Code generation for {typeName} failed");
                continue;
            }

            _csFileHandler.SaveOrReplace(file, builderFilePath);
            Console.WriteLine($"Code generation for {typeName} completed");
        }
    }

    private static Type? RetrieveTypeFromAssembly(string typeName, Assembly assembly)
    {
        var typeNameWithoutNamespace = typeName.Split('.')[^1];

        var types = assembly.GetTypes().Where(t => t.Name == typeNameWithoutNamespace).ToList();

        if (types.Count == 0)
        {
            Console.WriteLine($"No class with type name '{typeName}' found. Skipping {typeName}");
            return null;
        }

        if (types.Count > 1)
        {
            types = types.Where(t => t.FullName!.EndsWith(typeName)).ToList();
            if (types.Count > 1)
            {
                Console.WriteLine($"More than one class with type name '{typeName}' found. Add the namespace to your class name for unambiguous detection. Skipping {typeName}");
                return null;
            }
        }
        return types.Single();
    }

    protected abstract bool UpdateFile(CsFile file, Type type, string builderClassName);

    protected void RemoveAllGeneratedMethods(Class cls)
    {
        var methodsToRemove = cls.Methods
            .Where(m => !m.LeadingComments.Any(c => c.Value.ToLower().Contains("tcg keep")))
            .ToArray();

        foreach (var method in methodsToRemove)
        {
            cls.RemoveMethod(method);
        }
    }

    private string GenerateBuilderClassName(Type type)
    {
        if (string.IsNullOrWhiteSpace(Config.BuilderNamePattern))
            return $"{type.Name}Builder";

        var builderClassName = Config.BuilderNamePattern.Replace("{ClassName}", type.Name);

        if (!_csClassNameRegex.IsMatch(builderClassName))
            throw new InvalidOperationException(
                "The given builder name pattern does not provide a valid C# class name");

        return builderClassName;
    }
}