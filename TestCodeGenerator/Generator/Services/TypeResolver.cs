﻿namespace TestCodeGenerator.Generator.Services;

public class TypeResolver
{
    //public ResolvedType Resolve(ParameterInfo param)
    //{
    //    var enumerableReport = AnalyzeForEnumerable(param.ParameterType);

    //    if (enumerableReport.IsEnumerable || enumerableReport.ImplementsEnumerableAsInterface)
    //    {
    //        return GetEnumerableParameterTypeName(enumerableReport, param);
    //    }

    //    return GetTypeName(param.ParameterType, param.IsNullable(true));
    //}

    //private ResolvedType GetEnumerableParameterTypeName(EnumerableReport enumerableReport, ParameterInfo param)
    //{
    //    var resolvedGenericType = GetTypeName(enumerableReport.GenericArgumentTypes!, param.IsNullable(true));

    //    var resolvedType = new ResolvedType(param.ParameterType, param.ParameterType.Namespace!)
    //    {
    //        GenericArgumentTypes = enumerableReport.IsActualTypeGeneric ? resolvedGenericType : null,
    //        IsOrImplementsEnumerable = true,
    //        IsEnumerable = enumerableReport.IsEnumerable,
    //        IsNullable = param.IsNullable()
    //    };

    //    return resolvedType;
    //}

    //private ResolvedType GetTypeName(Type type, bool isNullable)
    //{
    //    var namespaces = new List<string> { type.Namespace! };

    //    if (isNullable)
    //    {
    //        var arg = type.GetGenericArguments().SingleOrDefault();
    //        if (arg is not null)
    //            namespaces.Add(arg.Namespace!);
    //    }

    //    return new ResolvedType(type, namespaces)
    //    {
    //        IsNullable = isNullable
    //    };
    //}

    //private EnumerableReport AnalyzeForEnumerable(Type type)
    //{
    //    if (type == typeof(string))
    //        return EnumerableReport.NoEnumerable(type);

    //    if (type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
    //        return new EnumerableReport(type, true, true, false, type.GetGenericArguments());

    //    foreach (Type intType in type.GetInterfaces())
    //    {
    //        if (intType.IsGenericType
    //            && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
    //        {
    //            return new EnumerableReport(type, false, type.IsGenericType, true, intType.GetGenericArguments());
    //        }
    //    }
    //    return EnumerableReport.NoEnumerable(type);
    //}
}