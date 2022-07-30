using System.Text;

namespace TestCodeGenerator.Generator.Services;

public class ResolvedType
{
    private readonly IReadOnlyCollection<string> _namespaces;

    public ResolvedType(Type originalType, string nameSpace, ResolvedType? genericArgumentType = null,
        bool isNullable = false, bool isEnumerable = false)
    {
        OriginalType = originalType;
        _namespaces = new List<string> { nameSpace };
        GenericArgumentType = genericArgumentType;
        IsNullable = isNullable;
        IsOrImplementsEnumerable = isEnumerable;
    }

    public ResolvedType(Type originalType, IEnumerable<string> namespaces, ResolvedType? genericArgumentType = null,
        bool isNullable = false, bool isEnumerable = false)
    {
        OriginalType = originalType;
        _namespaces = namespaces.ToList();
        GenericArgumentType = genericArgumentType;
        IsNullable = isNullable;
        IsOrImplementsEnumerable = isEnumerable;
    }

    public Type OriginalType { get; }
    public ResolvedType? GenericArgumentType { get; init; }
    public bool IsOrImplementsEnumerable { get; init; }
    public bool IsNullable { get; init; }
    public bool IsGeneric => GenericArgumentType is not null;

    public string GetFullName()
    {
        var nameBuilder = new StringBuilder();

        if (IsOrImplementsEnumerable)
        {
            if (IsGeneric)
                nameBuilder.Append($"{OriginalType.Name[..^2]}<{GenericArgumentType.GetFullName()}>");
            else
                nameBuilder.Append($"{OriginalType.Name}");
        }
        else
        {
            string? argName = null;

            if (IsNullable)
            {
                var arg = OriginalType.GetGenericArguments().SingleOrDefault();
                argName = ResolveTypeName(arg);
            }

            argName ??= ResolveTypeName(OriginalType);

            nameBuilder.Append(argName);
        }

        if (IsNullable)
            nameBuilder.Append('?');

        return nameBuilder.ToString();
    }

    public IEnumerable<string> GetAllNamespaces()
    {
        if (GenericArgumentType is null)
            return _namespaces;

        return _namespaces.Union(GenericArgumentType.GetAllNamespaces());
    }

    private static string? ResolveTypeName(Type? type)
    {
        if (type is null)
            return null;

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