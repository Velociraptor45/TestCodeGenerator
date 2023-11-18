using RefleCS.Nodes;

namespace TestCodeGenerator.Generator.Models;

public class EnumerableReport
{
    private readonly IDictionary<Type, (Func<TypeReport, string>, Using)> _enumerableEmptyInitializations =
        new Dictionary<Type, (Func<TypeReport, string>, Using)>
    {
        {
            typeof(IEnumerable<>),
            (
                (report) => $"Enumerable.Empty<{report.GenericTypeArgs.First().GetFullName()}>()",
                new("System.Linq")
            )
        },
        {
            typeof(IList<>),
            (
                (report) => $"new List<{report.GenericTypeArgs.First().GetFullName()}>()",
                new("System.Collections.Generic")
            )
        },
        {
            typeof(ICollection<>),
            (
                (report) => $"new List<{report.GenericTypeArgs.First().GetFullName()}>()",
                new("System.Collections.Generic")
            )
        },
        {
            typeof(IDictionary<,>),
            (
                (report) => $"new Dictionary<{report.GetGenericArgs()}>()",
                new("System.Collections.Generic")
            )
        },
        {
            typeof(IReadOnlyCollection<>),
            (
                (report) => $"new List<{report.GenericTypeArgs.First().GetFullName()}>()",
                new("System.Collections.Generic")
            )
        }
    };

    public EnumerableReport(Type type)
    {
        if (type == typeof(string))
        {
            IsIEnumerable = false;
            ImplementsIEnumerable = false;
            SupportsEmptyInitialization = false;
            return;
        }

        if (type.IsArray)
        {
            IsIEnumerable = false;
            ImplementsIEnumerable = false;
            SupportsEmptyInitialization = true;
            return;
        }

        if (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            IsIEnumerable = true;
            ImplementsIEnumerable = false;
            SupportsEmptyInitialization = true;
            return;
        }

        IsIEnumerable = false;

        if (AnyInterfaceImplementIEnumerable(type))
        {
            ImplementsIEnumerable = true;
            SupportsEmptyInitialization = HasEmptyInitializationSupport(type);
            return;
        }

        ImplementsIEnumerable = false;
        SupportsEmptyInitialization = false;
    }

    public bool IsIEnumerable { get; }
    public bool ImplementsIEnumerable { get; }
    public bool IsOrImplementsIEnumerable => IsIEnumerable || ImplementsIEnumerable;
    public bool SupportsEmptyInitialization { get; }

    private bool AnyInterfaceImplementIEnumerable(Type type)
    {
        return type
            .GetInterfaces()
            .Any(intType => intType.IsGenericType
                            && (intType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                || intType.GetInterfaces().Any(AnyInterfaceImplementIEnumerable)));
    }

    private bool HasEmptyInitializationSupport(Type type)
    {
        if (!type.IsGenericType)
            return false;

        type = type.GetGenericTypeDefinition();
        return _enumerableEmptyInitializations.ContainsKey(type);
    }

    public string GetEmptyInitialization(TypeReport typeReport, out Using @using)
    {
        if (typeReport.Type.IsArray)
        {
            var elementType = typeReport.Type.GetElementType()!;
            var resolvedElementType = TypeReport.ResolveTypeName(elementType);
            @using = new("System");
            return $"Array.Empty<{resolvedElementType}>()";
        }

        var entry = _enumerableEmptyInitializations[typeReport.Type.GetGenericTypeDefinition()];
        @using = entry.Item2;
        return entry.Item1(typeReport);
    }
}