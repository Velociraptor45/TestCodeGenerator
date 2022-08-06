namespace TestCodeGenerator.Generator.Services;

public class EnumerableReport
{
    public EnumerableReport(Type actualType, bool isEnumerable, bool isActualTypeGeneric,
        bool implementsEnumerableAsInterface, IEnumerable<Type> genericArgumentTypes)
    {
        if (isEnumerable && genericArgumentTypes is null)
            throw new ArgumentException("Non-generic enumerables are currently not supported");

        ActualType = actualType;
        IsEnumerable = isEnumerable;
        IsActualTypeGeneric = isActualTypeGeneric;
        ImplementsEnumerableAsInterface = implementsEnumerableAsInterface;
        GenericArgumentTypes = genericArgumentTypes.ToList();
    }

    public Type ActualType { get; }
    public bool IsEnumerable { get; }
    public bool IsActualTypeGeneric { get; }
    public bool ImplementsEnumerableAsInterface { get; }
    public IReadOnlyCollection<Type> GenericArgumentTypes { get; }

    public static EnumerableReport NoEnumerable(Type type)
    {
        return new EnumerableReport(type, false, false, false, Enumerable.Empty<Type>());
    }
}