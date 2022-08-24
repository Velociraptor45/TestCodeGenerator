using RefleCS;
using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Services;

namespace TestCodeGenerator.Generator.Generators;

public class TestBuilderGenerator
{
    private readonly IFileHandler _fileHandler;
    private readonly ICsFileHandler _csFileHandler;
    private readonly BuilderConfiguration _config;
    private readonly HashSet<string> _namespaces = new();

    public TestBuilderGenerator(IFileHandler fileHandler, ICsFileHandler csFileHandler, BuilderConfiguration config)
    {
        _fileHandler = fileHandler;
        _csFileHandler = csFileHandler;
        _config = config;
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
        AddNamespace(type.Namespace!);

        var file = GenerateFile(type);
        _csFileHandler.SaveOrReplace(file, Path.Combine(_config.OutputFolder, $"{type.Name}Builder.cs"));
    }

    private CsFile GenerateFile(Type type)
    {
        var builderClassName = $"{type.Name}Builder";

        var cls = Class.Public(builderClassName);
        cls.AddBaseType(new BaseType($"{_config.GenericSuperclassTypeName}<{type.Name}>"));

        AddMethods(type, builderClassName, cls);

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

    private void AddMethods(Type type, string builderClassName, Class cls)
    {
        var parameters = new Dictionary<(string, string), ParameterInfo>();

        foreach (var ctor in type.GetConstructors())
        {
            foreach (var parameter in ctor.GetParameters())
            {
                var key = (parameter.Name!, parameter.ParameterType.FullName!);
                if (parameters.ContainsKey(key))
                    continue;

                parameters.Add(key, parameter);
            }
        }

        foreach (var info in parameters.Values)
        {
            AddMethod(info, builderClassName, cls);
        }
    }

    private void AddMethod(ParameterInfo param, string builderClassName, Class cls)
    {
        NullabilityInfoContext context = new();
        var nullabilityInfo = context.Create(param);
        var typeReport = new TypeReport(param.ParameterType, nullabilityInfo);
        AddNamespaces(typeReport.GetAllNamespaces());

        if (typeReport.EnumerableReport.IsOrImplementsIEnumerable)
        {
            AddEnumerableParameterMethods(param, typeReport, builderClassName, cls);
            return;
        }

        AddMethod(param, typeReport, builderClassName, cls);
    }

    private void AddMethod(ParameterInfo param, TypeReport typeReport, string builderClassName, Class cls)
    {
        var capitalizedName = CapitalizeFirstLetter(param.Name!);

        var method = Method.Public(
            builderClassName,
            $"With{capitalizedName}",
            new List<Parameter> { new(typeReport.GetFullName(), param.Name!) },
            new List<Statement>
            {
                new($"{_config.CtorInjectionMethodName}(nameof({param.Name}), {param.Name});"),
                new("return this;")
            });
        cls.AddMethod(method);

        if (typeReport.NullabilityReport.IsNullable)
        {
            var withoutMethod = Method.Public(
                builderClassName,
                $"Without{capitalizedName}");
            withoutMethod.AddStatement(new($"return With{capitalizedName}(null);"));
            cls.AddMethod(withoutMethod);
        }
    }

    private void AddEnumerableParameterMethods(ParameterInfo param, TypeReport typeReport, string builderClassName,
        Class cls)
    {
        var capitalizedName = CapitalizeFirstLetter(param.Name!);

        // With
        var withMethod = Method.Public(
            builderClassName,
            $"With{capitalizedName}",
            new List<Parameter> { new(typeReport.GetFullName(), param.Name!) },
            new List<Statement>
            {
                new($"{_config.CtorInjectionMethodName}(nameof({param.Name}), {param.Name});"),
                new("return this;")
            });
        cls.AddMethod(withMethod);

        // WithEmpty
        if (typeReport.HasStandardCtor || typeReport.EnumerableReport.IsIEnumerable)
        {
            var emptyEnumerableInitialization = GetInitializationOfEmptyEnumerable(typeReport);

            var withEmptyMethod = Method.Public(
                builderClassName,
                $"WithEmpty{capitalizedName}");
            withEmptyMethod.AddStatement(new($"return With{capitalizedName}({emptyEnumerableInitialization});"));
            cls.AddMethod(withEmptyMethod);
        }
        else
        {
            Console.WriteLine(
                $"No standard ctor found for type {typeReport.Type.Name}. Skipping 'WithEmpty' method");
        }

        // Without
        if (typeReport.NullabilityReport.IsNullable)
        {
            var withoutMethod = Method.Public(
                builderClassName,
                $"Without{capitalizedName}");
            withoutMethod.AddStatement(new($"return With{capitalizedName}(null);"));
            cls.AddMethod(withoutMethod);
        }
    }

    private string GetInitializationOfEmptyEnumerable(TypeReport typeReport)
    {
        if (!typeReport.EnumerableReport.IsOrImplementsIEnumerable)
            throw new InvalidOperationException(
                "Cannot create statement for enumerable initialization when type doesn't implement IEnumerable");

        if (typeReport.IsGeneric)
        {
            if (!typeReport.EnumerableReport.IsIEnumerable)
            {
                return $"new {typeReport.Type.Name[..^2]}<{typeReport.GetGenericArgs()}>()";
            }

            return $"Enumerable.Empty<{typeReport.GenericTypeArgs.First().GetFullName()}>()";
        }

        return $"new {typeReport.Type.Name}()";
    }

    private void AddNamespace(string nmsp)
    {
        if (!_namespaces.Contains(nmsp))
            _namespaces.Add(nmsp);
    }

    private void AddNamespaces(IEnumerable<string> namespaces)
    {
        foreach (var nmsp in namespaces)
        {
            AddNamespace(nmsp);
        }
    }

    private string CapitalizeFirstLetter(string name)
    {
        return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
    }
}