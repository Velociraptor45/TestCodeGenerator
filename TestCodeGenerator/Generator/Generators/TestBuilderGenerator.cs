using System.Reflection;
using System.Text;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Extensions;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Services;

namespace TestCodeGenerator.Generator.Generators;

public class TestBuilderGenerator
{
    private readonly IFileHandler _fileHandler;
    private readonly TypeResolver _typeResolver;
    private readonly BuilderConfiguration _config;
    private readonly HashSet<string> _namespaces = new();

    public TestBuilderGenerator(IFileHandler fileHandler, TypeResolver typeResolver, BuilderConfiguration config)
    {
        _fileHandler = fileHandler;
        _typeResolver = typeResolver;
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

        var content = GetContent(type);

        _fileHandler.CreateFile(Path.Combine(_config.OutputFolder, $"{type.Name}Builder.cs"), content);
    }

    private string GetContent(Type type)
    {
        var builderClassName = $"{type.Name}Builder";

        var contentBuilder = new StringBuilder();

        contentBuilder.Append($@"
namespace {GetBuilderClassNamespace(type)};

public class {builderClassName} : {_config.GenericSuperclassTypeName}<{type.Name}>
{{
    {GetMethods(type, builderClassName)}
}}
");
        foreach (var nmsp in _namespaces.OrderByDescending(nmsp => nmsp))
        {
            contentBuilder.Insert(0, $"using {nmsp};{Environment.NewLine}");
        }

        return contentBuilder.ToString();
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

    private string GetMethods(Type type, string builderClassName)
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

        var methods = parameters.Values.Select(p => BuildMethod(p, builderClassName));

        return string.Join($@"{Environment.NewLine}
    ", methods);
    }

    private string BuildMethod(ParameterInfo param, string builderClassName)
    {
        //var resolvedType = _typeResolver.Resolve(param);
        NullabilityInfoContext context = new();
        var nullabilityInfo = context.Create(param);
        var typeReport = new TypeReport(param.ParameterType, nullabilityInfo);
        AddNamespaces(typeReport.GetAllNamespaces());

        if (typeReport.EnumerableReport.IsOrImplementsIEnumerable)
        {
            return BuildEnumerableParameterMethods(param, typeReport, builderClassName);
        }

        return BuildMethod(param, typeReport, builderClassName);
    }

    private string BuildMethod(ParameterInfo param, TypeReport typeReport, string builderClassName)
    {
        var strBuilder = new StringBuilder();
        var capitalizedName = CapitalizeFirstLetter(param.Name!);

        strBuilder.Append(@$"public {builderClassName} With{capitalizedName}({typeReport.GetFullName()} {param.Name})
    {{
        {_config.CtorInjectionMethodName}(nameof({param.Name}), {param.Name});
        return this;
    }}");

        if (param.IsNullable())
        {
            strBuilder.Append(@$"{Environment.NewLine}
    public {builderClassName} Without{capitalizedName}()
    {{
        return With{capitalizedName}(null);
    }}");
        }

        return strBuilder.ToString();
    }

    private string BuildEnumerableParameterMethods(ParameterInfo param, TypeReport typeReport, string builderClassName)
    {
        var strBuilder = new StringBuilder();
        var capitalizedName = CapitalizeFirstLetter(param.Name!);
        var emptyEnumerableInitialization = GetInitializationOfEmptyEnumerable(typeReport);

        // With
        strBuilder.Append(@$"public {builderClassName} With{capitalizedName}({typeReport.GetFullName()} {param.Name})
    {{
        {_config.CtorInjectionMethodName}(nameof({param.Name}), {param.Name});
        return this;
    }}");

        // WithEmpty
        if (emptyEnumerableInitialization is not null)
        {
            strBuilder.Append(@$"{Environment.NewLine}
    public {builderClassName} WithEmpty{capitalizedName}()
    {{
        return With{capitalizedName}({emptyEnumerableInitialization});
    }}");
        }

        // Without
        if (typeReport.NullabilityReport.IsNullable)
        {
            strBuilder.Append($@"{Environment.NewLine}
    public {builderClassName} Without{capitalizedName}()
    {{
        return With{capitalizedName}(null);
    }}");
        }

        return strBuilder.ToString();
    }

    private string? GetInitializationOfEmptyEnumerable(TypeReport typeReport)
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

            // todo: support for interfaces inheriting from IEnumerable<T>
            return $"Enumerable.Empty<{typeReport.GenericTypeArgs.First().GetFullName()}>()";
        }

        if (typeReport.Type.GetConstructors().All(c => c.GetParameters().Any()))
        {
            Console.WriteLine(
                $"No standard ctor found for type {typeReport.Type.Name}. Skipping 'WithEmpty' method");
            return null;
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