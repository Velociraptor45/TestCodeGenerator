namespace TestCodeGenerator.Generator.Services;

public class EnumeraReport
{
    public EnumeraReport(Type type)
    {
        if (type == typeof(string))
        {
            IsIEnumerable = false;
            ImplementsIEnumerable = false;
            return;
        }

        if (type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        {
            IsIEnumerable = true;
            ImplementsIEnumerable = false;
            return;
        }

        IsIEnumerable = false;

        if (AnyInterfaceImplementIEnumerable(type))
        {
            ImplementsIEnumerable = true;
            return;
        }

        ImplementsIEnumerable = false;
    }

    private bool AnyInterfaceImplementIEnumerable(Type type)
    {
        return type
            .GetInterfaces()
            .Any(intType => intType.IsGenericType
                            && (intType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                || intType.GetInterfaces().Any(AnyInterfaceImplementIEnumerable)));
    }

    public bool IsIEnumerable { get; }
    public bool ImplementsIEnumerable { get; }
    public bool IsOrImplementsIEnumerable => IsIEnumerable || ImplementsIEnumerable;
}