using Generator.Extensions;
using System.Reflection;

namespace Generator;

public class TypeResolver
{
    public ResolvedType Resolve(ParameterInfo param)
    {
        var enumerableReport = AnalyzeForEnumerable(param.ParameterType);

        if (enumerableReport.IsEnumerable)
        {
            return GetEnumerableParameterTypeName(enumerableReport, param);
        }

        return GetTypeName(param.ParameterType, param.IsNullable(true));
    }

    private ResolvedType GetEnumerableParameterTypeName(EnumerableReport enumerableReport, ParameterInfo param)
    {
        var resolvedGenericType = GetTypeName(enumerableReport.GenericArgumentType!, param.IsNullable(true));

        var resolvedType = new ResolvedType(param.ParameterType, param.ParameterType.Namespace!)
        {
            GenericArgumentType = enumerableReport.IsActualTypeGeneric ? resolvedGenericType : null,
            IsOrImplementsEnumerable = true,
            IsNullable = param.IsNullable()
        };

        return resolvedType;
    }

    private ResolvedType GetTypeName(Type type, bool isNullable)
    {
        var namespaces = new List<string> { type.Namespace! };

        if (isNullable)
        {
            var arg = type.GetGenericArguments().SingleOrDefault();
            if (arg is not null)
                namespaces.Add(arg.Namespace!);
        }

        return new ResolvedType(type, namespaces)
        {
            IsNullable = isNullable
        };
    }

    private EnumerableReport AnalyzeForEnumerable(Type type)
    {
        if (type == typeof(string))
            return EnumerableReport.NoEnumerable(type);

        if (type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            return new EnumerableReport(type, true, true, false, type.GetGenericArguments()[0]);

        foreach (Type intType in type.GetInterfaces())
        {
            if (intType.IsGenericType
                && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return new EnumerableReport(type, true, type.IsGenericType, true, intType.GetGenericArguments()[0]);
            }
        }
        return EnumerableReport.NoEnumerable(type);
    }
}