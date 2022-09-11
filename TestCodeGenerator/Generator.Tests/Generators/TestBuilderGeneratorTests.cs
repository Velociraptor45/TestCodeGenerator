using FluentAssertions;
using RefleCS;
using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Modules.TestBuilder;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.CtorParameterModule;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses.PublicPropertyModule;
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
    }

    [Theory]
    [MemberData(nameof(GenerateTestData))]
    public void Generate_WithIntCtor_ShouldSaveExpectedResult(string className, string expectedBuilder)
    {
        TestFolder.CreateTemp(folderPath =>
        {
            // Arrange
            var filePath = Path.Combine(folderPath, $"{className}Builder.cs");
            _fixture.SetupBuilderConfiguration(folderPath);
            _fixture.SetupFileHandlerLoadingAssembly();
            var sut = _fixture.CreateSut();

            // Act
            sut.Generate(className);

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

        public void SetupBuilderConfiguration(string outputFolder)
        {
            _builderConfiguration = new BuilderConfiguration
            {
                DllPath = "test.Path.dll",
                OutputFolder = outputFolder,
                GenericSuperclassTypeName = "DomainTestBuilderBase",
                GenericSuperclassNamespace = "Superclass.Namespace",
                CtorInjectionMethodName = "FillConstructorWith",
                PropertyInjectionMethodName = "FillPropertyWith",
                OutputAssemblyRootNamespace = "TestCodeGenerator.Generator.Tests.Tests"
            };
        }

        public void SetupFileHandlerLoadingAssembly()
        {
            TestPropertyNotSetException.ThrowIfNull(_builderConfiguration);

            _fileHandlerMock.Setup(m => m.LoadAssembly(_builderConfiguration.DllPath))
                .Returns(_assembly);
        }
    }
}