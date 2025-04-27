using FluentAssertions;
using RefleCS;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Modules.TestBuilder;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.NoRandomData;
using TestCodeGenerator.TestTools;
using TestCodeGenerator.TestTools.Exceptions;

namespace TestCodeGenerator.Generator.Tests.Generators;

public class NoRandomTestBuilderGeneratorTests
{
    private readonly NoRandomTestBuilderGeneratorFixture _fixture = new();

    public static IEnumerable<object[]> GenerateTestData()
    {
        yield return [nameof(BoolPropertyNoRandomTest), BoolPropertyNoRandomTest.GetExpectedBuilder()];
        yield return [nameof(AdditionalCtorNoRandomTest), AdditionalCtorNoRandomTest.GetExpectedBuilder()];
        yield return [nameof(PrivatelySettablePropertyNoRandomTest), PrivatelySettablePropertyNoRandomTest.GetExpectedBuilder()];
    }

    [Theory]
    [MemberData(nameof(GenerateTestData))]
    public void Generate_WithValidClass_ShouldSaveExpectedResult(string className, string expectedBuilder)
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");

            _fixture.SetupFileNotExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath);
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            sut.Generate(new List<string> { className });

            // Assert
            File.Exists(filePath).Should().BeTrue();
            var fileContent = File.ReadAllText(filePath);
            fileContent.Should().Be(expectedBuilder);
        });
    }

    [Theory]
    [InlineData(typeof(ClassWithoutDefaultCtor))]
    [InlineData(typeof(AbstractClass))]
    public void Generate_WithNoDefaultCtor_ShouldSkipClass(Type classType)
    {
        var className = classType.Name;
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");

            _fixture.SetupFileNotExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath);
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            sut.Generate(new List<string> { className });

            // Assert
            File.Exists(filePath).Should().BeFalse();
        });
    }

    [Fact]
    public void Generate_WithFileAlreadyExisting_ShouldSaveExpectedResult()
    {
        var className = nameof(BoolPropertyNoRandomTest);
        var expectedBuilder = BoolPropertyNoRandomTest.GetExpectedBuilder();
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");
            File.WriteAllText(filePath, expectedBuilder);

            _fixture.SetupFileExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath);
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            sut.Generate(new List<string> { className });

            // Assert
            File.Exists(filePath).Should().BeTrue();

            var fileContent = File.ReadAllText(filePath);
            fileContent.Should().Be(expectedBuilder);
        });
    }

    private class NoRandomTestBuilderGeneratorFixture
    {
        private readonly Mock<IFileHandler> _fileHandlerMock = new(MockBehavior.Strict);
        private BuilderConfiguration? _builderConfiguration;
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public NoRandomTestBuilderGenerator CreateSut()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            return new NoRandomTestBuilderGenerator(_fileHandlerMock.Object, new CsFileHandler(), _builderConfiguration,
                new List<ITestBuilderModule>
                {
                    new PublicPropertyModule(_builderConfiguration)
                });
        }

        public void SetupBuilderConfiguration(string outputFolder, string? builderNamePattern = null,
            bool nullabilityEnabled = true, bool matchFolderToNamespace = false)
        {
            _builderConfiguration = new BuilderConfiguration
            {
                DllPath = "test.Path.dll",
                OutputFolder = outputFolder,
                OutputAssemblyRootNamespace = "TestCodeGenerator.Generator.Tests.Tests",
                BuilderNamePattern = builderNamePattern,
                NullabilityEnabled = nullabilityEnabled,
                MatchFolderToNamespace = matchFolderToNamespace,
                UseRandomData = false
            };
        }

        public void SetupFileHandlerLoadingAssembly()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            _fileHandlerMock.Setup(m => m.LoadAssembly(_builderConfiguration.DllPath))
                .Returns(_assembly);
        }

        public void SetupFileExisting(string filePath)
        {
            _fileHandlerMock.Setup(m => m.FileExits(filePath)).Returns(true);
        }

        public void SetupFileNotExisting(string filePath)
        {
            _fileHandlerMock.Setup(m => m.FileExits(filePath)).Returns(false);
        }
    }

}

public class ClassWithoutDefaultCtor
{
    public ClassWithoutDefaultCtor(int x)
    {
    }
}

public abstract class AbstractClass
{
}