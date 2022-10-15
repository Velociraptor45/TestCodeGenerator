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
    private readonly Namespaces _usings = new();

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
        _usings.Add(_config.GenericSuperclassNamespace);

        var assembly = _fileHandler.LoadAssembly(_config.DllPath);

        var types = assembly.GetTypes().Where(t => t.Name == typeName).ToList();

        if (types.Count == 0)
            throw new ArgumentException($"No class with type name '{typeName}' found", nameof(typeName));
        if (types.Count > 1)
            throw new ArgumentException($"More than one class with type name '{typeName}' found. Further distinction is currently not implemented", nameof(typeName));

        var type = types.Single();
        _usings.Add(type.Namespace!);

        var builderClassName = GenerateBuilderClassName(type);
        var builderFilePath = Path.Combine(_config.OutputFolder, $"{builderClassName}.cs");

        var file = _fileHandler.FileExits(builderFilePath)
            ? _csFileHandler.FromFile(builderFilePath)
            : new CsFile(Enumerable.Empty<Using>(), new Namespace(GetBuilderClassNamespace(type)));

        UpdateFile(file, type, builderClassName);
        _csFileHandler.SaveOrReplace(file, builderFilePath);
    }

    private void UpdateFile(CsFile file, Type type, string builderClassName)
    {
        var cls = file.Nmsp.Classes.FirstOrDefault(c => c.Name == builderClassName);

        if (cls is null)
        {
            cls = Class.Public(builderClassName);
            file.Nmsp.AddClass(cls);
        }

        cls.RemoveAllBaseTypes();
        cls.AddBaseType(new BaseType($"{_config.GenericSuperclassTypeName}<{type.Name}>"));

        var methodsToRemove = cls.Methods
            .Where(m => !m.LeadingComments.Any(c => c.Value.ToLower().Contains("tcg keep")))
            .ToArray();

        foreach (var method in methodsToRemove)
        {
            cls.RemoveMethod(method);
        }

        foreach (var module in _modules)
        {
            module.Apply(type, builderClassName, cls, _usings);
        }

        foreach (var @using in _usings)
        {
            file.AddUsing(new Using(@using));
        }

        file.OrderUsingsAsc();
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