using Generator.Files;
using System.Reflection;

namespace Generator;

public class TestBuilderGenerator
{
    private readonly IFileHandler _fileHandler;

    public TestBuilderGenerator(IFileHandler fileHandler)
    {
        _fileHandler = fileHandler;
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
        var paramId = type.GetConstructors().First().GetParameters().First();
        var paramAv = type.GetConstructors().First().GetParameters().First(p => p.Name == "availabilities");

        var src = $@" // Auto-generated code
using System;

namespace {type.Namespace}.Test
{{
    public class {builderClassName} : DomainTestBuilderBase<{type.Name}>
    {{
        {GetParameterMethods(paramId, builderClassName)}

        {GetParameterMethods(paramAv, builderClassName)}
    }}
}}
";
        return src;
    }

    private string GetParameterMethods(ParameterInfo param, string builderClassName)
    {
        var enumerableType = GetEnumerableType(param.ParameterType);

        if (enumerableType != null)
        {
            return GetEnumerableParameterMethods(enumerableType, param, builderClassName);
        }

        var src = @$"public {builderClassName} With{CapitalizeFirstLetter(param.Name!)}({param.ParameterType.Name} {param.Name})
        {{
            FillConstructorWith(nameof({param.Name}), {param.Name});
            return this;
        }}";

        return src;
    }

    private string GetEnumerableParameterMethods(Type genericType, ParameterInfo param, string builderClassName)
    {
        var parameterType = $"{param.ParameterType.Name.Substring(0, param.ParameterType.Name.Length - 2)}<{genericType.Name}>";

        var src = @$"public {builderClassName} With{CapitalizeFirstLetter(param.Name!)}({parameterType} {param.Name})
        {{
            FillConstructorWith(nameof({param.Name}), {param.Name});
            return this;
        }}

        public {builderClassName} Without{CapitalizeFirstLetter(param.Name!)}()
        {{
            return With{CapitalizeFirstLetter(param.Name!)}(Enumerable.Empty<{genericType.Name}>();
        }}";

        return src;
    }

    private string CapitalizeFirstLetter(string name)
    {
        return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
    }

    private Type? GetEnumerableType(Type type)
    {
        if (type == typeof(string))
            return null;

        if (type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return type.GetGenericArguments()[0];
        foreach (Type intType in type.GetInterfaces())
        {
            if (intType.IsGenericType
                && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return intType.GetGenericArguments()[0];
            }
        }
        return null;
    }
}