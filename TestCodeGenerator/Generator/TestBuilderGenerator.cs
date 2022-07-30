using Generator.Configurations;
using Generator.Extensions;
using Generator.Files;
using System.Reflection;
using System.Text;

namespace Generator;

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

        var bytes = _fileHandler.LoadDll(_config.DllPath);
        var assembly = Assembly.Load(bytes);

        var types = assembly.GetTypes().Where(t => t.Name == typeName).ToList();

        if (types.Count == 0)
            throw new Exception($"No class with type name {typeName} found");
        if (types.Count > 1)
            throw new Exception($"More than one class with type name {typeName} found. Further distinction is currently not implemented");

        var type = types.Single();

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

        var methods = parameters.Values.Select(p => GetParameterMethods(p, builderClassName));

        return string.Join($@"{Environment.NewLine}
    ", methods);
    }

    private string GetParameterMethods(ParameterInfo param, string builderClassName)
    {
        var resolvedType = _typeResolver.Resolve(param);
        AddNamespaces(resolvedType.GetAllNamespaces());

        if (resolvedType.IsOrImplementsEnumerable)
        {
            return GetEnumerableParameterMethods(param, resolvedType, builderClassName);
        }

        return GetParameterMethod(param, resolvedType, builderClassName);
    }

    private string GetParameterMethod(ParameterInfo param, ResolvedType resolvedType, string builderClassName)
    {
        var capitalizedName = CapitalizeFirstLetter(param.Name!);

        var src = @$"public {builderClassName} With{capitalizedName}({resolvedType.GetFullName()} {param.Name})
    {{
        {_config.CtorInjectionMethodName}(nameof({param.Name}), {param.Name});
        return this;
    }}";

        if (param.IsNullable())
        {
            src += @$"{Environment.NewLine}
    public {builderClassName} Without{capitalizedName}()
    {{
        return With{capitalizedName}(null);
    }}";
        }

        return src;
    }

    private string GetEnumerableParameterMethods(ParameterInfo param, ResolvedType resolvedType, string builderClassName)
    {
        var contentBuilder = new StringBuilder();
        var capitalizedName = CapitalizeFirstLetter(param.Name!);
        var emptyEnumerableInitialization = GetInitializationOfEmptyEnumerable(resolvedType);

        contentBuilder.Append(@$"public {builderClassName} With{capitalizedName}({resolvedType.GetFullName()} {param.Name})
    {{
        {_config.CtorInjectionMethodName}(nameof({param.Name}), {param.Name});
        return this;
    }}");

        if (emptyEnumerableInitialization is not null)
        {
            contentBuilder.Append(@$"{Environment.NewLine}
    public {builderClassName} WithEmpty{capitalizedName}()
    {{
        return With{capitalizedName}({emptyEnumerableInitialization});
    }}");
        }

        if (resolvedType.IsNullable)
        {
            contentBuilder.Append($@"{Environment.NewLine}
    public {builderClassName} Without{capitalizedName}()
    {{
        return With{capitalizedName}(null);
    }}");
        }

        return contentBuilder.ToString();
    }

    private string? GetInitializationOfEmptyEnumerable(ResolvedType resolvedType)
    {
        if (!resolvedType.IsOrImplementsEnumerable)
            throw new InvalidOperationException(
                "Cannot create statement for enumerable initialization when type doesn't implement IEnumerable");

        if (resolvedType.IsGeneric)
        {
            // todo: distinguish between List, Dictionary, IEnumerable, etc.
            return $"Enumerable.Empty<{resolvedType.GenericArgumentType!.GetFullName()}>()";
        }

        if (resolvedType.OriginalType.GetConstructors().All(c => c.GetParameters().Any()))
        {
            Console.WriteLine(
                $"No standard ctor found for type {resolvedType.OriginalType.Name}. Skipping 'WithEmpty' method");
            return null;
        }

        return $"new {resolvedType.OriginalType.Name}()";
    }

    private void AddNamespaces(IEnumerable<string> namespaces)
    {
        foreach (var namesp in namespaces)
        {
            if (!_namespaces.Contains(namesp))
                _namespaces.Add(namesp);
        }
    }

    private string CapitalizeFirstLetter(string name)
    {
        return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
    }
}