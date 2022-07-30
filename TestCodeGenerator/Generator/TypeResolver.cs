using Generator.Extensions;
using System.Reflection;

namespace Generator;

public class TypeResolver
{
    public ResolvedType Resolve(ParameterInfo param)
    {
        var enumerableType = GetEnumerableType(param.ParameterType);

        if (enumerableType is not null)
        {
            return GetEnumerableParameterTypeName(enumerableType, param);
        }

        return GetTypeName(param.ParameterType, param.IsNullable(true));
    }

    private ResolvedType GetEnumerableParameterTypeName(Type genericType, ParameterInfo param)
    {
        var resolvedGenericType = GetTypeName(genericType, param.IsNullable(true));

        var resolvedType = new ResolvedType(param.ParameterType, param.ParameterType.Namespace!)
        {
            GenericArgumentType = resolvedGenericType,
            IsEnumerable = true,
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