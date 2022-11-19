using System.Reflection;

namespace TestCodeGenerator.Generator.Models;

public class NullabilityReport
{
    private readonly bool _nullabilityEnabled;
    private readonly bool _isClass;

    public NullabilityReport(Type type, NullabilityInfo? nullabilityInfo, bool nullabilityEnabled)
    {
        _isClass = type.IsClass;
        _nullabilityEnabled = nullabilityEnabled;
        HasNullableGenericType = type.IsGenericType
                                 && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (HasNullableGenericType)
        {
            IsNullable = true;
            return;
        }

        if (!nullabilityEnabled)
        {
            IsNullable = _isClass;
            return;
        }

        IsNullable = nullabilityInfo is null
                     || (nullabilityInfo.WriteState == NullabilityState.Nullable
                         && nullabilityInfo.ReadState == NullabilityState.Nullable);
    }

    public bool IsNullable { get; }
    public bool HasNullableGenericType { get; }

    public string GetNullabilityPostfix()
    {
        if (!IsNullable)
            return string.Empty;

        if (_isClass && !_nullabilityEnabled)
            return string.Empty;

        return "?";
    }
}