using Generator.Extensions;
using Generator.Files;
using System.Reflection;

namespace Generator;

public class TestBuilderGenerator
{
    private readonly IFileHandler _fileHandler;
    private readonly TypeResolver _typeResolver;

    public TestBuilderGenerator(IFileHandler fileHandler, TypeResolver typeResolver)
    {
        _fileHandler = fileHandler;
        _typeResolver = typeResolver;
    }

    public void Generate(string dllPath, string typeName, string outputFolder)
    {
        try
        {
            var bytes = _fileHandler.LoadDll(dllPath);
            var assembly = Assembly.Load(bytes);

            var types = assembly.GetTypes().Where(t => t.Name == typeName).ToList();

            if (types.Count == 0)
                throw new Exception($"No class with type name {typeName} found");
            if (types.Count > 1)
                throw new Exception($"More than one class with type name {typeName} found");

            var type = types.Single();

            var content = GetContent(type);

            _fileHandler.CreateFile(Path.Combine(outputFolder, $"{type.Name}Builder.cs"), content);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private string GetContent(Type type)
    {
        var builderClassName = $"{type.Name}Builder";

        var src = $@" // Auto-generated code
using System;

namespace {type.Namespace}.Test;

public class {builderClassName} : DomainTestBuilderBase<{type.Name}>
{{
    {GetMethods(type, builderClassName)}
}}
";
        return src;
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

        if (resolvedType.IsEnumerable)
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
        FillConstructorWith(nameof({param.Name}), {param.Name});
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
        var capitalizedName = CapitalizeFirstLetter(param.Name!);

        var src = @$"public {builderClassName} With{capitalizedName}({resolvedType.GetFullName()} {param.Name})
    {{
        FillConstructorWith(nameof({param.Name}), {param.Name});
        return this;
    }}

    public {builderClassName} WithEmpty{capitalizedName}()
    {{
        return With{capitalizedName}(Enumerable.Empty<{resolvedType.GenericArgumentType.GetFullName()}>());
    }}";

        return src;
    }

    private string CapitalizeFirstLetter(string name)
    {
        return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
    }
}