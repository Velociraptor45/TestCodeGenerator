using System.Reflection;
using System.Text;

namespace TestCodeGenerator.Generator.Models;

public class TypeReport
{
    public TypeReport(Type type, NullabilityInfo nullabilityInfo, bool nullabilityEnabled)
    {
        Type = type;
        NullabilityReport = new NullabilityReport(type, nullabilityInfo, nullabilityEnabled);
        EnumerableReport = new EnumerableReport(type);

        GenericTypeArgs = GetTypeReportsForGenericArguments(type, nullabilityInfo, nullabilityEnabled).ToList();
    }

    private IEnumerable<TypeReport> GetTypeReportsForGenericArguments(Type type,
        NullabilityInfo nullabilityInfo, bool nullabilityEnabled)
    {
        if (!type.IsGenericType || NullabilityReport.HasNullableGenericType)
            yield break;

        var genArgs = type.GetGenericArguments();

        for (var i = 0; i < genArgs.Length; i++)
        {
            yield return new TypeReport(genArgs[i], nullabilityInfo.GenericTypeArguments[i], nullabilityEnabled);
        }
    }

    public Type Type { get; }
    public bool IsGeneric => GenericTypeArgs.Any();
    public NullabilityReport NullabilityReport { get; }
    public IReadOnlyCollection<TypeReport> GenericTypeArgs { get; }
    public EnumerableReport EnumerableReport { get; }

    public bool HasStandardCtor => Type.GetConstructors().Any(c => !c.GetParameters().Any());

    public IEnumerable<string> GetAllNamespaces()
    {
        var namespaces = new List<string> { GetNamespace() };

        if (!IsGeneric)
            return namespaces;

        return namespaces.Union(GenericTypeArgs.SelectMany(arg => arg.GetAllNamespaces()));
    }

    public string GetFullName()
    {
        var builder = new StringBuilder();
        if (IsGeneric)
        {
            builder.Append($"{Type.Name[..^2]}<{GetGenericArgs()}>");
        }
        else
        {
            if (NullabilityReport.HasNullableGenericType)
            {
                var resolvedTypeName = ResolveTypeName(Type.GetGenericArguments().Single());
                builder.Append(resolvedTypeName);
            }
            else
            {
                builder.Append(ResolveTypeName(Type));
            }
        }

        builder.Append(NullabilityReport.GetNullabilityPostfix());

        return builder.ToString();
    }

    public string GetGenericArgs()
    {
        if (!IsGeneric)
            return string.Empty;

        return string.Join(", ", GenericTypeArgs.Select(arg => arg.GetFullName()));
    }

    private string GetNamespace()
    {
        if (NullabilityReport.HasNullableGenericType)
        {
            return Type.GetGenericArguments().First().Namespace!;
        }

        return Type.Namespace!;
    }

    private static string ResolveTypeName(Type type)
    {
        _typeAlias.TryGetValue(type, out var argName);
        return argName ?? type.Name;
    }

    private static readonly Dictionary<Type, string> _typeAlias = new()
    {
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(char), "char" },
        { typeof(decimal), "decimal" },
        { typeof(double), "double" },
        { typeof(float), "float" },
        { typeof(int), "int" },
        { typeof(long), "long" },
        { typeof(object), "object" },
        { typeof(sbyte), "sbyte" },
        { typeof(short), "short" },
        { typeof(string), "string" },
        { typeof(uint), "uint" },
        { typeof(ulong), "ulong" },
        { typeof(void), "void" }
    };
}