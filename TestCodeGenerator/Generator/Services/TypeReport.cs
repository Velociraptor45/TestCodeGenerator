﻿using System.Reflection;
using System.Text;

namespace TestCodeGenerator.Generator.Services;

public class TypeReport
{
    public TypeReport(Type type, NullabilityInfo nullabilityInfo)
    {
        Type = type;
        NullabilityReport = new NullabilityReport(type, nullabilityInfo);
        EnumerableReport = new EnumeraReport(type);

        GenericTypeArgs = GetTypeReportsForGenericArguments(type, nullabilityInfo).ToList();
    }

    private IEnumerable<TypeReport> GetTypeReportsForGenericArguments(Type type,
        NullabilityInfo nullabilityInfo)
    {
        if (!type.IsGenericType || NullabilityReport.HasNullableGenericType)
            yield break;

        var genArgs = type.GetGenericArguments();

        for (var i = 0; i < genArgs.Length; i++)
        {
            yield return new TypeReport(genArgs[i], nullabilityInfo.GenericTypeArguments[i]);
        }
    }

    public Type Type { get; }
    public bool IsGeneric => GenericTypeArgs.Any();
    public NullabilityReport NullabilityReport { get; }
    public IReadOnlyCollection<TypeReport> GenericTypeArgs { get; }
    public EnumeraReport EnumerableReport { get; }

    public IEnumerable<string> GetAllNamespaces()
    {
        var namespaces = new List<string> { Type.Namespace! };

        if (!IsGeneric)
            return namespaces;

        return namespaces.Union(GenericTypeArgs.SelectMany(arg => arg.GetAllNamespaces()));
    }

    public string GetFullName()
    {
        var builder = new StringBuilder();
        if (!IsGeneric)
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
        else
        {
            // todo
        }

        if (NullabilityReport.IsNullable)
            builder.Append('?');

        return builder.ToString();
    }

    public string GetGenericArgs()
    {
        if (!IsGeneric)
            return string.Empty;

        return string.Join(", ", GenericTypeArgs.Select(arg => arg.GetFullName()));
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