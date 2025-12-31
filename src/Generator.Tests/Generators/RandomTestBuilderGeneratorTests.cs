using FluentAssertions;
using MyNamespace.Folder;
using RefleCS;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Modules.TestBuilder;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CustomizedBuilderName;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.ExistingFile;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.Records;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.WithoutNullability;
using TestCodeGenerator.Generator.Tests.Tests;
using TestCodeGenerator.MyNamespace.Folder;
using TestCodeGenerator.MyNamespace.Folder.Sub.Sub.Inside.Another.Folder.Here;
using TestCodeGenerator.TestTools;
using TestCodeGenerator.TestTools.Exceptions;
using DuplicatedClassNameTest = TestCodeGenerator.Generator.Tests.Generators.TestClasses.DuplicatedName.DuplicatedClassNameTest;

namespace TestCodeGenerator.Generator.Tests.Generators;

public class RandomTestBuilderGeneratorTests
{
    private readonly RandomTestBuilderGeneratorFixture _fixture = new();

    public static IEnumerable<object?[]> GenerateWithExistingFileTestData()
    {
        yield return
        [
            nameof(ExistingFileTest),
            ExistingFileTest.GetExpectedBuilder(),
            ExistingFileTest.GetExistingBuilder()
        ];
        yield return
        [
            nameof(ExistingFileWithMethodToKeepTest),
            ExistingFileWithMethodToKeepTest.GetExpectedBuilder(),
            ExistingFileWithMethodToKeepTest.GetExistingBuilder()
        ];
    }

    public static IEnumerable<object?[]> GenerateWithBuilderNameCustomizationTestData()
    {
        yield return
        [
            nameof(WithClassNamePatternTest),
            WithClassNamePatternTest.GetExpectedBuilder(),
            WithClassNamePatternTest.GetBuilderNamePattern(),
            WithClassNamePatternTest.GetFileName()
        ];
        yield return
        [
            nameof(WithoutClassNamePatternTest),
            WithoutClassNamePatternTest.GetExpectedBuilder(),
            WithoutClassNamePatternTest.GetBuilderNamePattern(),
            WithoutClassNamePatternTest.GetFileName()
        ];
        yield return
        [
            nameof(WithStaticClassNamePatternTest),
            WithStaticClassNamePatternTest.GetExpectedBuilder(),
            WithStaticClassNamePatternTest.GetBuilderNamePattern(),
            WithStaticClassNamePatternTest.GetFileName()
        ];
    }

    public static IEnumerable<object?[]> GenerateWithoutNullabilityTestData()
    {
        yield return
        [
            nameof(ClassParameterTest),
            ClassParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(StructParameterTest),
            StructParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(NullableStructParameterTest),
            NullableStructParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(IntParameterForNullabilityTest),
            IntParameterForNullabilityTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(NullableIntParameterForNullabilityTest),
            NullableIntParameterForNullabilityTest.GetExpectedBuilder()
        ];
    }

    public static IEnumerable<object[]> GenerateTestData()
    {
        // Ctor
        yield return [nameof(IntParameterTest), IntParameterTest.GetExpectedBuilder()];
        yield return [nameof(NullableIntParameterTest), NullableIntParameterTest.GetExpectedBuilder()];
        yield return [nameof(BoolParameterTest), BoolParameterTest.GetExpectedBuilder()];
        yield return [nameof(NullableEnumerableParameterTest), NullableEnumerableParameterTest.GetExpectedBuilder()];
        yield return
        [
            nameof(NullableEnumerableWithNullableArgParameterTest), NullableEnumerableWithNullableArgParameterTest.GetExpectedBuilder()
        ];
        yield return [nameof(ListParameterTest), ListParameterTest.GetExpectedBuilder()];
        yield return [nameof(DictionaryParameterTest), DictionaryParameterTest.GetExpectedBuilder()];
        yield return [nameof(EnumerableParameterTest), EnumerableParameterTest.GetExpectedBuilder()];
        yield return [nameof(IListParameterTest), IListParameterTest.GetExpectedBuilder()];
        yield return [nameof(IDictionaryParameterTest), IDictionaryParameterTest.GetExpectedBuilder()];
        yield return [nameof(ICollectionParameterTest), ICollectionParameterTest.GetExpectedBuilder()];
        yield return [nameof(IReadOnlyCollectionParameterTest), IReadOnlyCollectionParameterTest.GetExpectedBuilder()];
        yield return [nameof(ArrayParameterTest), ArrayParameterTest.GetExpectedBuilder()];
        yield return [nameof(ArrayInArrayParameterTest), ArrayInArrayParameterTest.GetExpectedBuilder()];
        yield return
        [
            nameof(InheritFromIEnumerableWithStandardCtorParameterTest),
            InheritFromIEnumerableWithStandardCtorParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(InheritFromIEnumerableWithoutStandardCtorParameterTest),
            InheritFromIEnumerableWithoutStandardCtorParameterTest.GetExpectedBuilder()
        ];
        yield return [nameof(NullableDictionaryParameterTest), NullableDictionaryParameterTest.GetExpectedBuilder()];
        yield return
        [
            nameof(NullableDictionaryWithSecondArgNullableParameterTest),
            NullableDictionaryWithSecondArgNullableParameterTest.GetExpectedBuilder()
        ];
        yield return [nameof(SingleGenericParameterTest), SingleGenericParameterTest.GetExpectedBuilder()];
        yield return [nameof(DoubleGenericParameterTest), DoubleGenericParameterTest.GetExpectedBuilder()];
        yield return
        [
            nameof(NullableEnumerableInEnumerableParameterTest), NullableEnumerableInEnumerableParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(EnumerableInEnumerableParameterTest), EnumerableInEnumerableParameterTest.GetExpectedBuilder()
        ];
        yield return [nameof(ListInEnumerableParameterTest), ListInEnumerableParameterTest.GetExpectedBuilder()];
        yield return [nameof(EnumerableInListParameterTest), EnumerableInListParameterTest.GetExpectedBuilder()];
        yield return
        [
            nameof(DoubleGenericInEnumerableParameterTest), DoubleGenericInEnumerableParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(SingleGenericInDoubleGenericParameterTest), SingleGenericInDoubleGenericParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(DuplicatedCtorParametersTest), DuplicatedCtorParametersTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(DuplicatedCtorParameterTypesTest), DuplicatedCtorParameterTypesTest.GetExpectedBuilder()
        ];

        // property
        yield return [nameof(IntPropertyTest), IntPropertyTest.GetExpectedBuilder()];
        yield return [nameof(NullableIntPropertyTest), NullableIntPropertyTest.GetExpectedBuilder()];
        yield return [nameof(BoolPropertyTest), BoolPropertyTest.GetExpectedBuilder()];
        yield return [nameof(NullableEnumerablePropertyTest), NullableEnumerablePropertyTest.GetExpectedBuilder()];
        yield return
        [
            nameof(NullableEnumerableWithNullableArgPropertyTest), NullableEnumerableWithNullableArgPropertyTest.GetExpectedBuilder()
        ];
        yield return [nameof(ListPropertyTest), ListPropertyTest.GetExpectedBuilder()];
        yield return [nameof(DictionaryPropertyTest), DictionaryPropertyTest.GetExpectedBuilder()];
        yield return [nameof(EnumerablePropertyTest), EnumerablePropertyTest.GetExpectedBuilder()];
        yield return
        [
            nameof(InheritFromIEnumerableWithStandardCtorPropertyTest),
            InheritFromIEnumerableWithStandardCtorPropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(InheritFromIEnumerableWithoutStandardCtorPropertyTest),
            InheritFromIEnumerableWithoutStandardCtorPropertyTest.GetExpectedBuilder()
        ];
        yield return [nameof(NullableDictionaryPropertyTest), NullableDictionaryPropertyTest.GetExpectedBuilder()];
        yield return
        [
            nameof(NullableDictionaryWithSecondArgNullablePropertyTest),
            NullableDictionaryWithSecondArgNullablePropertyTest.GetExpectedBuilder()
        ];
        yield return [nameof(SingleGenericPropertyTest), SingleGenericPropertyTest.GetExpectedBuilder()];
        yield return [nameof(DoubleGenericPropertyTest), DoubleGenericPropertyTest.GetExpectedBuilder()];
        yield return
        [
            nameof(NullableEnumerableInEnumerablePropertyTest), NullableEnumerableInEnumerablePropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(EnumerableInEnumerablePropertyTest), EnumerableInEnumerablePropertyTest.GetExpectedBuilder()
        ];
        yield return [nameof(ListInEnumerablePropertyTest), ListInEnumerablePropertyTest.GetExpectedBuilder()];
        yield return [nameof(EnumerableInListPropertyTest), EnumerableInListPropertyTest.GetExpectedBuilder()];
        yield return
        [
            nameof(DoubleGenericInEnumerablePropertyTest), DoubleGenericInEnumerablePropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(SingleGenericInDoubleGenericPropertyTest), SingleGenericInDoubleGenericPropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(DuplicatedCtorParametersTest), DuplicatedCtorParametersTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(DuplicatedCtorParameterTypesTest), DuplicatedCtorParameterTypesTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(PrivatePropertyTest), PrivatePropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(GetOnlyPropertyTest), GetOnlyPropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(PrivateSetPropertyTest), PrivateSetPropertyTest.GetExpectedBuilder()
        ];

        // record
        yield return
        [
            nameof(RecordOnlyParameterTest), RecordOnlyParameterTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(RecordWithAdditionalPropertyTest), RecordWithAdditionalPropertyTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(RecordWithAdditionalCtorTest), RecordWithAdditionalCtorTest.GetExpectedBuilder()
        ];

        // namespaces
        yield return
        [
            nameof(DifferentNamespaceTest), DifferentNamespaceTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(PartiallyDifferentNamespaceTest), PartiallyDifferentNamespaceTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(NamespaceLongerThanConfigTest), NamespaceLongerThanConfigTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(NamespaceSubsetOfConfigTest), NamespaceSubsetOfConfigTest.GetExpectedBuilder()
        ];
        yield return
        [
            nameof(NamespaceSameAsConfigTest), NamespaceSameAsConfigTest.GetExpectedBuilder()
        ];
    }

    [Theory]
    [MemberData(nameof(GenerateTestData))]
    public void Generate_ShouldSaveExpectedResult(string className, string expectedBuilder)
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
    [MemberData(nameof(GenerateWithBuilderNameCustomizationTestData))]
    public void Generate_WithValidBuilderNameCustomization_ShouldSaveExpectedResult(string className, string expectedBuilder,
        string? builderNamePattern, string fileName)
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{fileName}.cs");

            _fixture.SetupFileNotExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath, builderNamePattern);
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

    [Fact]
    public void Generate_WithInvalidBuilderNameCustomization_ShouldThrow()
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            _fixture.SetupBuilderConfiguration(folderPath, "{ClassName}with/=Builder");
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            var func = () => sut.Generate(new List<string> { nameof(WithClassNamePatternTest) });

            // Assert
            func.Should().ThrowExactly<InvalidOperationException>()
                .WithMessage("The given builder name pattern does not provide a valid C# class name");
        });
    }

    [Theory]
    [MemberData(nameof(GenerateWithExistingFileTestData))]
    public void Generate_WithExistingFile_ShouldSaveExpectedResult(string className, string expectedBuilder,
        string existingBuilder)
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");

            File.WriteAllText(filePath, existingBuilder);

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

    [Theory]
    [MemberData(nameof(GenerateWithoutNullabilityTestData))]
    public void Generate_WithoutNullability_ShouldSaveExpectedResult(string className, string expectedBuilder)
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");

            _fixture.SetupFileNotExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath, nullabilityEnabled: false);
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

    [Fact]
    public void Generate_WithMatchFolderToNamespaceActive_ShouldPlaceFileInCorrectFolder()
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var className = nameof(IntParameterTest);
            var expectedBuilder = IntParameterTest.GetExpectedBuilder();
            var filePath = Path.Combine(folderPath, "Generators", "TestClasses", "CtorParameterModule", $"{className}Builder.cs");

            _fixture.SetupFileNotExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath, matchFolderToNamespace: true);
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

    [Fact]
    public void Generate_WithFileWithInvalidCsAlreadyExisting_ShouldSaveExpectedResult()
    {
        var className = nameof(IntParameterTest);
        var expectedBuilder = IntParameterTest.GetExpectedBuilder();
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");
            File.WriteAllText(filePath, "This is no valid C#");

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

    [Fact]
    public void Generate_WithDuplicatedClassName_ShouldNotCreateFile()
    {
        var className = nameof(DuplicatedClassNameTest);
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");

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
    public void Generate_WithDuplicatedClassName_WithNamespaceAddition_ShouldNotCreateFile()
    {
        var className = $"{nameof(DuplicatedClassNameTest)}";
        var namespaceName = $"DuplicatedName.{className}";
        var expectedBuilder = DuplicatedClassNameTest.GetExpectedBuilder();
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");

            _fixture.SetupFileNotExisting(filePath);
            _fixture.SetupBuilderConfiguration(folderPath);
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            sut.Generate(new List<string> { namespaceName });

            // Assert
            File.Exists(filePath).Should().BeTrue();
            var fileContent = File.ReadAllText(filePath);
            fileContent.Should().Be(expectedBuilder);
        });
    }


    private class RandomTestBuilderGeneratorFixture
    {
        private readonly Mock<IFileHandler> _fileHandlerMock = new(MockBehavior.Strict);
        private BuilderConfiguration? _builderConfiguration;
        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public RandomTestBuilderGenerator CreateSut()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            return new RandomTestBuilderGenerator(_fileHandlerMock.Object, new CsFileHandler(), _builderConfiguration,
                new List<ITestBuilderModule>
                {
                    new PublicPropertyModule(_builderConfiguration),
                    new CtorParameterModule(_builderConfiguration)
                });
        }

        public void SetupBuilderConfiguration(string outputFolder, string? builderNamePattern = null,
            bool nullabilityEnabled = true, bool matchFolderToNamespace = false)
        {
            _builderConfiguration = new BuilderConfiguration
            {
                DllPath = "test.Path.dll",
                OutputFolder = outputFolder,
                GenericSuperclassTypeName = "DomainTestBuilderBase",
                GenericSuperclassNamespace = "Superclass.Namespace",
                CtorInjectionMethodName = "FillConstructorWith",
                PropertyInjectionMethodName = "FillPropertyWith",
                OutputAssemblyRootNamespace = "TestCodeGenerator.Generator.Tests.Tests",
                BuilderNamePattern = builderNamePattern,
                NullabilityEnabled = nullabilityEnabled,
                MatchFolderToNamespace = matchFolderToNamespace
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