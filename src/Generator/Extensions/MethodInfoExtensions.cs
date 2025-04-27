using System.Reflection;
using System.Runtime.CompilerServices;

namespace TestCodeGenerator.Generator.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsInitOnly(this MethodInfo method)
    {
        return method.ReturnParameter.GetRequiredCustomModifiers().Contains(typeof(IsExternalInit));
    }
}
