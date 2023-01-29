using RefleCS.Nodes;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Extensions;
using TestCodeGenerator.Generator.Models;

namespace TestCodeGenerator.Generator.Modules.TestBuilder;

public abstract class TestBuilderModuleBase : ITestBuilderModule
{
    protected readonly BuilderConfiguration Config;

    protected TestBuilderModuleBase(BuilderConfiguration config)
    {
        Config = config;
    }

    public abstract void Apply(Type type, string builderClassName, Class cls, Namespaces namespaces);

    protected abstract Statement GetWithStatement(string originalName, string withMethodParameterName);

    protected string GetParameterName(string originalName)
    {
        return originalName.LowercaseFirstLetter();
    }

    protected void AddMethods(NullabilityInfo nullabilityInfo, Type type, string name, string builderClassName,
        Class cls, Namespaces namespaces)
    {
        var typeReport = new TypeReport(type, nullabilityInfo, Config.NullabilityEnabled);

        if (DoesMethodAlreadyExist(cls, $"With{name.CapitalizeFirstLetter()}", typeReport.GetFullName()))
            return;

        namespaces.AddRange(typeReport.GetAllNamespaces());

        AddMethods(name, typeReport, builderClassName, cls);
    }

    private void AddMethods(string name, TypeReport typeReport, string builderClassName, Class cls)
    {
        var capitalizedName = name.CapitalizeFirstLetter();
        var withMethodParameterName = GetParameterName(name);

        // With
        var withMethod = Method.Public(
            builderClassName,
            $"With{capitalizedName}",
            new List<Parameter> { new(typeReport.GetFullName(), withMethodParameterName) },
            new List<Statement>
            {
                GetWithStatement(name, withMethodParameterName),
                new("return this;")
            });
        cls.AddMethod(withMethod);

        // WithEmpty
        if (typeReport.EnumerableReport.IsOrImplementsIEnumerable)
        {
            AddWithEmptyMethod(capitalizedName, typeReport, builderClassName, cls);
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

    private void AddWithEmptyMethod(string capitalizedName, TypeReport typeReport, string builderClassName, Class cls)
    {
        if (!typeReport.HasStandardCtor && !typeReport.EnumerableReport.IsIEnumerable)
        {
            Console.WriteLine(
                $"No standard ctor found for type {typeReport.GetFullName()}. Skipping 'WithEmpty' method");
            return;
        }

        var emptyEnumerableInitialization = GetInitializationOfEmptyEnumerable(typeReport);

        var withEmptyMethod = Method.Public(
            builderClassName,
            $"WithEmpty{capitalizedName}");
        withEmptyMethod.AddStatement(new($"return With{capitalizedName}({emptyEnumerableInitialization});"));
        cls.AddMethod(withEmptyMethod);
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

    private bool DoesMethodAlreadyExist(Class cls, string methodName, string parameterTypeName)
    {
        return cls.Methods.Any(m =>
            m.Name == methodName
            && m.Parameters.Count == 1
            && m.Parameters.First().TypeName == parameterTypeName);
    }
}