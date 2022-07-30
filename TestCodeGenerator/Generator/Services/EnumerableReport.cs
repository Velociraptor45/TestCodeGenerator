namespace TestCodeGenerator.Generator.Services;

public class EnumerableReport
{
    public EnumerableReport(Type actualType, bool isEnumerable, bool isActualTypeGeneric,
        bool implementsEnumerableAsInterface, Type? genericArgumentType)
    {
        if (isEnumerable && genericArgumentType is null)
            throw new ArgumentException("Non-generic enumerables are currently not supported");

        ActualType = actualType;
        IsEnumerable = isEnumerable;
        IsActualTypeGeneric = isActualTypeGeneric;
        ImplementsEnumerableAsInterface = implementsEnumerableAsInterface;
        GenericArgumentType = genericArgumentType;
    }

    public Type ActualType { get; }
    public bool IsEnumerable { get; }
    public bool IsActualTypeGeneric { get; }
    public bool ImplementsEnumerableAsInterface { get; }
    public Type? GenericArgumentType { get; }

    public static EnumerableReport NoEnumerable(Type type)
    {
        return new(type, false, false, false, null);
    }
}