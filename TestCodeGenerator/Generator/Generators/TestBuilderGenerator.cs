using RefleCS;
using RefleCS.Nodes;
using System.Text.RegularExpressions;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Models;
using TestCodeGenerator.Generator.Modules.TestBuilder;

namespace TestCodeGenerator.Generator.Generators;

public class TestBuilderGenerator
{
    private readonly Regex _csClassNameRegex = new(@"^[A-Za-z_][A-Za-z0-9_]*$");

    private readonly IFileHandler _fileHandler;
    private readonly ICsFileHandler _csFileHandler;
    private readonly BuilderConfiguration _config;
    private readonly List<ITestBuilderModule> _modules;
    private readonly Namespaces _namespaces = new();

    public TestBuilderGenerator(IFileHandler fileHandler, ICsFileHandler csFileHandler, BuilderConfiguration config,
        IEnumerable<ITestBuilderModule> modules)
    {
        _fileHandler = fileHandler;
        _csFileHandler = csFileHandler;
        _config = config;
        _modules = modules.ToList();
    }

    public void Generate(string typeName)
    {
        _namespaces.Add(_config.GenericSuperclassNamespace);

        var assembly = _fileHandler.LoadAssembly(_config.DllPath);

        var types = assembly.GetTypes().Where(t => t.Name == typeName).ToList();

        if (types.Count == 0)
            throw new ArgumentException($"No class with type name '{typeName}' found", nameof(typeName));
        if (types.Count > 1)
            throw new ArgumentException($"More than one class with type name '{typeName}' found. Further distinction is currently not implemented", nameof(typeName));

        var type = types.Single();
        _namespaces.Add(type.Namespace!);

        var builderClassName = GenerateBuilderClassName(type);
        var file = GenerateFile(type, builderClassName);
        _csFileHandler.SaveOrReplace(file, Path.Combine(_config.OutputFolder, $"{builderClassName}.cs"));
    }

    private CsFile GenerateFile(Type type, string builderClassName)
    {
        var cls = Class.Public(builderClassName);
        cls.AddBaseType(new BaseType($"{_config.GenericSuperclassTypeName}<{type.Name}>"));

        foreach (var module in _modules)
        {
            module.Apply(type, builderClassName, cls, _namespaces);
        }

        var nmsp = new Namespace(GetBuilderClassNamespace(type), new List<Class> { cls });

        var file = new CsFile(
            _namespaces.OrderBy(n => n).Select(n => new Using(n)),
            nmsp);

        return file;
    }

    private string GetBuilderClassNamespace(Type type)
    {
        var rootNamespace = _config.OutputAssemblyRootNamespace;
        var typeNamespace = type.Namespace!;

        for (int i = 0; i < _config.OutputAssemblyRootNamespace.Length; i++)
        {
            if (rootNamespace[i] == typeNamespace[i])
                continue;

            return $"{rootNamespace}.{typeNamespace[i..]}";
        }

        throw new InvalidOperationException("Namespace detection failed. Debug me");
    }

    private string GenerateBuilderClassName(Type type)
    {
        if (string.IsNullOrWhiteSpace(_config.BuilderNamePattern))
            return $"{type.Name}Builder";

        var builderClassName = _config.BuilderNamePattern.Replace("{ClassName}", type.Name);

        if (!_csClassNameRegex.IsMatch(builderClassName))
            throw new InvalidOperationException(
                "The given builder name pattern does not provide a valid C# class name");

        return builderClassName;
    }
}