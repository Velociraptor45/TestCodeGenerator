using System.Reflection;
using TestCodeGenerator.Generator.Configurations;
using TestCodeGenerator.Generator.Files;
using TestCodeGenerator.Generator.Generators;
using TestCodeGenerator.Generator.Tests.Generators.TestClasses;

namespace TestCodeGenerator.Generator.Tests.Generators;

public class TestBuilderGeneratorTests
{
    private readonly TestBuilderGeneratorFixture _fixture;

    public TestBuilderGeneratorTests()
    {
        _fixture = new TestBuilderGeneratorFixture();
    }

    private static IEnumerable<object[]> GenerateTestData()
    {
        yield return new object[] { nameof(IntTest), IntTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableIntTest), NullableIntTest.GetExpectedBuilder() };
        yield return new object[] { nameof(BoolTest), BoolTest.GetExpectedBuilder() };
        yield return new object[] { nameof(NullableEnumerableTest), NullableEnumerableTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableWithNullableArgTest), NullableEnumerableWithNullableArgTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListTest), ListTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DictionaryTest), DictionaryTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableTest), EnumerableTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithStandardCtorTest),
            InheritFromIEnumerableWithStandardCtorTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(InheritFromIEnumerableWithoutStandardCtorTest),
            InheritFromIEnumerableWithoutStandardCtorTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(NullableDictionaryTest), NullableDictionaryTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableDictionaryWithSecondArgNullableTest),
            NullableDictionaryWithSecondArgNullableTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(SingleGenericTest), SingleGenericTest.GetExpectedBuilder() };
        yield return new object[] { nameof(DoubleGenericTest), DoubleGenericTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(NullableEnumerableInEnumerableTest), NullableEnumerableInEnumerableTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(EnumerableInEnumerableTest), EnumerableInEnumerableTest.GetExpectedBuilder()
        };
        yield return new object[] { nameof(ListInEnumerableTest), ListInEnumerableTest.GetExpectedBuilder() };
        yield return new object[] { nameof(EnumerableInListTest), EnumerableInListTest.GetExpectedBuilder() };
        yield return new object[]
        {
            nameof(DoubleGenericInEnumerableTest), DoubleGenericInEnumerableTest.GetExpectedBuilder()
        };
        yield return new object[]
        {
            nameof(SingleGenericInDoubleGenericTest), SingleGenericInDoubleGenericTest.GetExpectedBuilder()
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
        // Arrange
        _fixture.SetupFileHandlerLoadingAssembly();
        _fixture.SetupFileHandlerCreatingFile(className, expectedBuilder);
        var sut = _fixture.CreateSut();

        // Act
        sut.Generate(className);

        // Assert
        _fixture.VerifyFileHandlerCreatingFile(className, expectedBuilder);
    }

    private class TestBuilderGeneratorFixture
    {
        private readonly Mock<IFileHandler> _fileHandlerMock = new(MockBehavior.Strict);
        private readonly BuilderConfiguration _builderConfiguration;
        private readonly Assembly _assembly;

        public TestBuilderGeneratorFixture()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _builderConfiguration = new BuilderConfiguration
            {
                DllPath = "test.Path.dll",
                OutputFolder = "MyOutputFolder",
                GenericSuperclassTypeName = "DomainTestBuilderBase",
                GenericSuperclassNamespace = "Superclass.Namespace",
                CtorInjectionMethodName = "FillConstructorWith",
                OutputAssemblyRootNamespace = "TestCodeGenerator.Generator.Tests.Tests"
            };
        }

        public TestBuilderGenerator CreateSut()
        {
            return new TestBuilderGenerator(_fileHandlerMock.Object, _builderConfiguration);
        }

        public void SetupFileHandlerLoadingAssembly()
        {
            _fileHandlerMock.Setup(m => m.LoadAssembly(_builderConfiguration.DllPath))
                .Returns(_assembly);
        }

        public void SetupFileHandlerCreatingFile(string className, string content)
        {
            _fileHandlerMock.Setup(m => m.CreateFile(@$"MyOutputFolder\{className}Builder.cs", content));
        }

        public void VerifyFileHandlerCreatingFile(string className, string content)
        {
            _fileHandlerMock
                .Verify(m => m.CreateFile(@$"MyOutputFolder\{className}Builder.cs", content), Times.Once);
        }
    }
}