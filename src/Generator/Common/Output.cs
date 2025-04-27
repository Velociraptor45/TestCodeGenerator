namespace TestCodeGenerator.Generator.Common;

public static class Output
{
    public static void WriteError(string text)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = color;
    }
}
