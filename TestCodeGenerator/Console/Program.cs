using CommandLine;
using Microsoft.Extensions.Configuration;
using TestCodeGenerator.Console;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;

Parser.Default.ParseArguments<CliOptions>(args)
    .WithParsed(o =>
    {
        var allConfigs = new List<BuilderConfiguration>();
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build()
            .GetSection("Settings")
            .Bind(allConfigs);

        var configsMatchingName = allConfigs.Where(c => c.Name == o.SettingsName).ToList();
        if (configsMatchingName.Count > 1)
        {
            Console.WriteLine($"Multiple settings with the name {o.SettingsName} found");
            return;
        }
        if (!configsMatchingName.Any())
        {
            Console.WriteLine($"No setting with the name {o.SettingsName} found");
            return;
        }

        new TestBuilderGenerator(new FileHandler(), configsMatchingName.First())
            .Generate(o.ClassName);
    });