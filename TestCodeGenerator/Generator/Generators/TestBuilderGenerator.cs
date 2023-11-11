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

    public TestBuilderGenerator(IFileHandler fileHandler, ICsFileHandler csFileHandler, BuilderConfiguration config,
        IEnumerable<ITestBuilderModule> modules)
    {
        _fileHandler = fileHandler;
        _csFileHandler = csFileHandler;
        _config = config;
        _modules = modules.ToList();
    }

    public void Generate(IEnumerable<string> typeNames)
    {
        var assembly = _fileHandler.LoadAssembly(_config.DllPath);

        var typeNamesList = typeNames.ToList();
        foreach (var typeName in typeNamesList)
        {
            Console.WriteLine();

            var types = assembly.GetTypes().Where(t => t.Name == typeName).ToList();

            if (types.Count == 0)
            {
                Console.WriteLine($"No class with type name '{typeName}' found. Skipping {typeName}");
                continue;
            }

            if (types.Count > 1)
            {
                Console.WriteLine($"More than one class with type name '{typeName}' found. Further distinction is currently not implemented. Skipping {typeName}");
                continue;
            }

            Console.WriteLine($"Starting code generation for {typeName}");
            var type = types.Single();

            var usings = new Namespaces { _config.GenericSuperclassNamespace, type.Namespace! };

            var builderClassName = GenerateBuilderClassName(type);

            var namespaceInfo = new NamespaceInfo(type, _config.OutputAssemblyRootNamespace);
            var builderFilePath = _config.MatchFolderToNamespace
                ? Path.Combine(_config.OutputFolder, namespaceInfo.InAssemblyPath.Replace('.', Path.DirectorySeparatorChar), $"{builderClassName}.cs")
                : Path.Combine(_config.OutputFolder, $"{builderClassName}.cs");

            var file = _fileHandler.FileExits(builderFilePath)
                ? _csFileHandler.FromFile(builderFilePath)
                    ?? new CsFile(Enumerable.Empty<Using>(), new Namespace(namespaceInfo.BuilderClassNamespace))
                : new CsFile(Enumerable.Empty<Using>(), new Namespace(namespaceInfo.BuilderClassNamespace));

            UpdateFile(file, type, usings, builderClassName);
            _csFileHandler.SaveOrReplace(file, builderFilePath);
            Console.WriteLine($"Code generation for {typeName} completed");
        }
    }

    private void UpdateFile(CsFile file, Type type, Namespaces usings, string builderClassName)
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
            module.Apply(type, builderClassName, cls, usings);
        }

        foreach (var @using in usings)
        {
            file.AddUsing(new Using(@using));
        }

        file.OrderUsingsAsc();
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