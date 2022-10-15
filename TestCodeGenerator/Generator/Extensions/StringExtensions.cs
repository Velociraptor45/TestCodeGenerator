namespace TestCodeGenerator.Generator.Extensions;

internal static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string name)
    {
        return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
    }

    public static string LowercaseFirstLetter(this string name)
    {
        return string.Concat(name[0].ToString().ToLower(), name.AsSpan(1));
    }
}