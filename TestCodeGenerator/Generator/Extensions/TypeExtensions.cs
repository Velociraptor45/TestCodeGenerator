using System.Reflection;

namespace TestCodeGenerator.Generator.Extensions;

public static class TypeExtensions
{
    public static bool IsNullable(this ParameterInfo param, bool evaluateGenericArguments = false)
    {
        var isNullabilityType = param.ParameterType.IsGenericType
            && param.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (isNullabilityType)
            return true;

        NullabilityInfoContext context = new();
        var nullabilityInfo = context.Create(param);

        if (evaluateGenericArguments)
            nullabilityInfo = nullabilityInfo.GenericTypeArguments.FirstOrDefault();

        if (nullabilityInfo is null)
            return false;

        return nullabilityInfo.WriteState == NullabilityState.Nullable
               && nullabilityInfo.ReadState == NullabilityState.Nullable;
    }
}