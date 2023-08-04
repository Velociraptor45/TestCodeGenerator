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

namespace TestCodeGenerator.Generator.Tests.Generators;

public class TestBuilderGeneratorTests
{
    private readonly TestBuilderGeneratorFixture _fixture;

    public TestBuilderGeneratorTests()
    {
        _fixture = new TestBuilderGeneratorFixture();
    }

    public static IEnumerable<object?[]> GenerateWithExistingFileTestData()
    {
        yield return new object[]
        {
            nameof(ExistingFileTest),
            ExistingFileTest.GetExpectedBuilder(),
            ExistingFileTest.GetExistingBuilder()
        };
        yield return new object[]
        {
            nameof(ExistingFileWithMethodToKeepTest),
            ExistingFileWithMethodToKeepTest.GetExpectedBuilder(),
            ExistingFileWithMethodToKeepTest.GetExistingBuilder()
        };
    }

    public static IEnumerable<object?[]> GenerateWithBuilderNameCustomizationTestData()
    {
        yield return new object[]
        {
            nameof(WithClassNamePatternTest),
            WithClassNamePatternTest.GetExpectedBuilder(),
            WithClassNamePatternTest.GetBuilderNamePattern(),
            WithClassNamePatternTest.GetFileName()
        };
        yield return new object?[]
        {
            nameof(WithoutClassNamePatternTest),
            WithoutClassNamePatternTest.GetExpectedBuilder(),
            WithoutClassNamePatternTest.GetBuilderNamePattern(),
            WithoutClassNamePatternTest.GetFileName()
        };
        yield return new object[]
        {
            nameof(WithStaticClassNamePatternTest),
            WithStaticClassNamePatternTest.GetExpectedBuilder(),
            WithStaticClassNamePatternTest.GetBuilderNamePattern(),
            WithStaticClassNamePatternTest.GetFileName()
        };
    }

    public static IEnumerable<object?[]> GenerateWithoutNullabilityTestData()
    {
        yield return new object[]
        {
            nameof(ClassParameterTest),
            ClassParameterTest.GetExpectedBuilder(),
        };
        yield return new object[]
        {
            nameof(StructParameterTest),
            StructParameterTest.GetExpectedBuilder(),
        };
        yield return new object[]
        {
            nameof(NullableStructParameterTest),
            NullableStructParameterTest.GetExpectedBuilder(),
        };
        yield return new object[]
        {
            nameof(IntParameterForNullabilityTest),
            IntParameterForNullabilityTest.GetExpectedBuilder(),
        };
        yield return new object[]
        {
            nameof(NullableIntParameterForNullabilityTest),
            NullableIntParameterForNullabilityTest.GetExpectedBuilder(),
        };
    }

    public static IEnumerable<object[]> GenerateTestData()
    {
        // Ctor
        yield return new object[] { nameof(IntParameterTest), IntParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableIntParameterTest), NullableIntParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(BoolParameterTest), BoolParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableEnumerableParameterTest), NullableEnumerableParameterTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableWithNullableArgParameterTest), NullableEnumerableWithNullableArgParameterTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListParameterTest), ListParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DictionaryParameterTest), DictionaryParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableParameterTest), EnumerableParameterTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithStandardCtorParameterTest),
            InheritFromIEnumerableWithStandardCtorParameterTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithoutStandardCtorParameterTest),
            InheritFromIEnumerableWithoutStandardCtorParameterTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(NullableDictionaryParameterTest), NullableDictionaryParameterTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableDictionaryWithSecondArgNullableParameterTest),
            NullableDictionaryWithSecondArgNullableParameterTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(SingleGenericParameterTest), SingleGenericParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DoubleGenericParameterTest), DoubleGenericParameterTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableInEnumerableParameterTest), NullableEnumerableInEnumerableParameterTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(EnumerableInEnumerableParameterTest), EnumerableInEnumerableParameterTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListInEnumerableParameterTest), ListInEnumerableParameterTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableInListParameterTest), EnumerableInListParameterTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(DoubleGenericInEnumerableParameterTest), DoubleGenericInEnumerableParameterTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(SingleGenericInDoubleGenericParameterTest), SingleGenericInDoubleGenericParameterTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(DuplicatedCtorParametersTest), DuplicatedCtorParametersTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(DuplicatedCtorParameterTypesTest), DuplicatedCtorParameterTypesTest.GetExpectedBuilder()
        };

        // property
        yield return new object[] { nameof(IntPropertyTest), IntPropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableIntPropertyTest), NullableIntPropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(BoolPropertyTest), BoolPropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableEnumerablePropertyTest), NullableEnumerablePropertyTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableWithNullableArgPropertyTest), NullableEnumerableWithNullableArgPropertyTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListPropertyTest), ListPropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DictionaryPropertyTest), DictionaryPropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerablePropertyTest), EnumerablePropertyTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithStandardCtorPropertyTest),
            InheritFromIEnumerableWithStandardCtorPropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithoutStandardCtorPropertyTest),
            InheritFromIEnumerableWithoutStandardCtorPropertyTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(NullableDictionaryPropertyTest), NullableDictionaryPropertyTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableDictionaryWithSecondArgNullablePropertyTest),
            NullableDictionaryWithSecondArgNullablePropertyTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(SingleGenericPropertyTest), SingleGenericPropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DoubleGenericPropertyTest), DoubleGenericPropertyTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableInEnumerablePropertyTest), NullableEnumerableInEnumerablePropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(EnumerableInEnumerablePropertyTest), EnumerableInEnumerablePropertyTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListInEnumerablePropertyTest), ListInEnumerablePropertyTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableInListPropertyTest), EnumerableInListPropertyTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(DoubleGenericInEnumerablePropertyTest), DoubleGenericInEnumerablePropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(SingleGenericInDoubleGenericPropertyTest), SingleGenericInDoubleGenericPropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(DuplicatedCtorParametersTest), DuplicatedCtorParametersTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(DuplicatedCtorParameterTypesTest), DuplicatedCtorParameterTypesTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(PrivatePropertyTest), PrivatePropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(GetOnlyPropertyTest), GetOnlyPropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(PrivateSetPropertyTest), PrivateSetPropertyTest.GetExpectedBuilder()
        };

        // record
        yield return new object[]
        {
            nameof(RecordOnlyParameterTest), RecordOnlyParameterTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(RecordWithAdditionalPropertyTest), RecordWithAdditionalPropertyTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(RecordWithAdditionalCtorTest), RecordWithAdditionalCtorTest.GetExpectedBuilder()
        };

        // namespaces
        yield return new object[]
        {
            nameof(DifferentNamespaceTest), DifferentNamespaceTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(PartiallyDifferentNamespaceTest), PartiallyDifferentNamespaceTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(NamespaceLongerThanConfigTest), NamespaceLongerThanConfigTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(NamespaceSubsetOfConfigTest), NamespaceSubsetOfConfigTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(NamespaceSameAsConfigTest), NamespaceSameAsConfigTest.GetExpectedBuilder()
        };
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

    private class TestBuilderGeneratorFixture
    {
        private readonly Mock<IFileHandler> _fileHandlerMock = new(MockBehavior.Strict);
        private BuilderConfiguration? _builderConfiguration;
        private readonly Assembly _assembly;

        public TestBuilderGeneratorFixture()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public TestBuilderGenerator CreateSut()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            return new TestBuilderGenerator(_fileHandlerMock.Object, new CsFileHandler(), _builderConfiguration,
                new List<ITestBuilderModule>
                {
                    new CtorParameterModule(_builderConfiguration),
                    new PublicPropertyModule(_builderConfiguration)
                });
        }

        public void SetupBuilderConfiguration(string outputFolder, string? builderNamePattern = null,
            bool nullabilityEnabled = true)
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
                NullabilityEnabled = nullabilityEnabled
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