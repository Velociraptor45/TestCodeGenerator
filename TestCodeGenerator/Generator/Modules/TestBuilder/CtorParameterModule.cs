using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Models;
using TestCodeGenerator.Generator.Services;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public class CtorParameterModule : ITestBuilderModule
{
    private readonly BuilderConfiguration _config;

    public CtorParameterModule(BuilderConfiguration config)
    {
        _config = config;
    }

    public void Apply(Type type, string builderClassName, Class cls, Namespaces namespaces)
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
            AddMethod(info, builderClassName, cls, namespaces);
        }
    }

    private void AddMethod(ParameterInfo param, string builderClassName, Class cls, Namespaces namespaces)
    {
        NullabilityInfoContext context = new();
        var nullabilityInfo = context.Create(param);
        var typeReport = new TypeReport(param.ParameterType, nullabilityInfo);
        namespaces.AddRange(typeReport.GetAllNamespaces());

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

    private string CapitalizeFirstLetter(string name)
    {
        return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
    }
}