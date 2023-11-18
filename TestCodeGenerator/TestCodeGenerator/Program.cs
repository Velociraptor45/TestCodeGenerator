using CommandLine;
using Microsoft.Extensions.Configuration;
using RefleCS;
using TestCodeGenerator;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Modules.TestBuilder;

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

        var classNames = o.ClassName.Split(',').Select(c => c.Trim());

        var config = configsMatchingName.First();
        new TestBuilderGenerator(new FileHandler(), new CsFileHandler(), config,
                new List<ITestBuilderModule>
                {
                    new CtorParameterModule(config),
                    new PublicPropertyModule(config)
                })
            .Generate(classNames);
    });
