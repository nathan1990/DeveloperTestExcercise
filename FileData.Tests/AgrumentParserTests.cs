using FileData.Interfaces;
using FileData.Operations;
using NUnit.Framework;
using Rhino.Mocks;

namespace FileData.Tests
{
    [TestFixture]
    public class ArgumentsParserTests
    {
        private IOperationTypeParser _mockOperationTypeParser;
        private ArgumentsParser _sut;
        private Arguments _parsedArguments;
        private string[] _dummyArgs;

        private const string DUMMY_OPERATION = "DummyOperationString";
        private const string DUMMY_FILE_PATH = @"C:\SomeDummy\FilePath";
        private OperationType DUMMY_OPERATION_TYPE = OperationType.GetSize;

        [SetUp]
        public void Setup()
        {
            _mockOperationTypeParser = MockRepository.GenerateMock<IOperationTypeParser>();
            _dummyArgs = new [] { DUMMY_OPERATION, DUMMY_FILE_PATH };
            _sut = new ArgumentsParser(_mockOperationTypeParser);
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void returns_false_when_not_two_arguments(int numberOfArguments)
        {
            var dummyArgs = new string[numberOfArguments];

            var result = _sut.TryParseAgruments(dummyArgs, out _parsedArguments);

            Assert.False(result);
        }

        [Test]
        public void returns_true_when_two_arguments()
        {
            var result = _sut.TryParseAgruments(_dummyArgs, out _parsedArguments);

            Assert.True(result);
        }

        [Test]
        public void calls_operations_parser()
        {
            _mockOperationTypeParser.Expect(m => m.Parse(DUMMY_OPERATION))
                .Return(DUMMY_OPERATION_TYPE).Repeat.Once();

            var unsued = _sut.TryParseAgruments(_dummyArgs, out _parsedArguments);

            _mockOperationTypeParser.VerifyAllExpectations();
        }

        [Test]
        public void correctly_populates_arguments_class()
        {
            _mockOperationTypeParser.Stub(m => m.Parse(DUMMY_OPERATION))
                .Return(DUMMY_OPERATION_TYPE);

            var unused = _sut.TryParseAgruments(_dummyArgs, out _parsedArguments);

            Assert.AreEqual(_parsedArguments.OperationToPerfom, DUMMY_OPERATION_TYPE);
            Assert.AreEqual(_parsedArguments.FilePath, DUMMY_FILE_PATH);
        }
    }
}
