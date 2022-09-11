using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Models;
using TestCodeGenerator.Generator.Services;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public class PublicPropertyModule : ITestBuilderModule
{
    private readonly BuilderConfiguration _config;

    public PublicPropertyModule(BuilderConfiguration config)
    {
        _config = config;
    }

    public void Apply(Type type, string builderClassName, Class cls, Namespaces namespaces)
    {
        var properties = new Dictionary<(string, string), PropertyInfo>();

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite))
        {
            var key = (property.Name, property.PropertyType.FullName!);
            properties.Add(key, property);
        }

        foreach (var info in properties.Values)
        {
            AddMethod(info, builderClassName, cls, namespaces);
        }
    }

    private void AddMethod(PropertyInfo prop, string builderClassName, Class cls, Namespaces namespaces)
    {
        NullabilityInfoContext context = new();
        var nullabilityInfo = context.Create(prop);
        var typeReport = new TypeReport(prop.PropertyType, nullabilityInfo);
        namespaces.AddRange(typeReport.GetAllNamespaces());

        if (typeReport.EnumerableReport.IsOrImplementsIEnumerable)
        {
            AddEnumerableParameterMethods(prop.Name, typeReport, builderClassName, cls);
            return;
        }

        AddMethod(prop.Name, typeReport, builderClassName, cls);
    }

    private void AddMethod(string name, TypeReport typeReport, string builderClassName, Class cls)
    {
        var capitalizedName = CapitalizeFirstLetter(name);
        var lowercasedName = LowercaseFirstLetter(name);

        var method = Method.Public(
            builderClassName,
            $"With{capitalizedName}",
            new List<Parameter> { new(typeReport.GetFullName(), lowercasedName) },
            new List<Statement>
            {
                new($"{_config.PropertyInjectionMethodName}(p => p.{name}, {lowercasedName});"),
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

    private void AddEnumerableParameterMethods(string name, TypeReport typeReport, string builderClassName,
        Class cls)
    {
        var capitalizedName = CapitalizeFirstLetter(name);
        var lowercasedName = LowercaseFirstLetter(name);

        // With
        var withMethod = Method.Public(
            builderClassName,
            $"With{capitalizedName}",
            new List<Parameter> { new(typeReport.GetFullName(), lowercasedName) },
            new List<Statement>
            {
                new($"{_config.PropertyInjectionMethodName}(p => p.{name}, {lowercasedName});"),
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
                $"No standard ctor found for type {typeReport.GetFullName()}. Skipping 'WithEmpty' method");
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

    private string LowercaseFirstLetter(string name)
    {
        return string.Concat(name[0].ToString().ToLower(), name.AsSpan(1));
    }
}