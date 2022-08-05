using CommandLine;

namespace TestCodeGenerator.Console;

public class CliOptions
{
    [Option('c', "class", Required = true, HelpText = "The name of the class you want to create a builder for")]
    public string ClassName { get; set; } = string.Empty;

    [Option('s', "settings", Required = true,
        HelpText = "The name of settings object that you want to use in your appsettings file")]
    public string SettingsName { get; set; } = string.Empty;
}