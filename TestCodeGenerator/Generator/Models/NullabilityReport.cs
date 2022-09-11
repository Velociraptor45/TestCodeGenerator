using System.Reflection;

namespace TestCodeGenerator.Generator.Models;

public class NullabilityReport
{
    public NullabilityReport(Type type, NullabilityInfo? nullabilityInfo)
    {
        HasNullableGenericType = type.IsGenericType
            && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (HasNullableGenericType)
        {
            IsNullable = true;
            return;
        }

        IsNullable = nullabilityInfo is null
                     || (nullabilityInfo.WriteState == NullabilityState.Nullable
                         && nullabilityInfo.ReadState == NullabilityState.Nullable);
    }

    public bool IsNullable { get; }
    public bool HasNullableGenericType { get; }
}