using FileData.Interfaces;
using FileData.Operations;
using NUnit.Framework;
using Rhino.Mocks;

namespace FileData.Tests
{
    [TestFixture]
    public class ApplicationTests
    {
        private IFileDetailsWrapper _mockFileDetailsWrapper;
        private IArgumentsParser _mockArgumentsParser;
        private IConsoleWrapper _stubConsoleWrapper;
        private Application _sut;
        private Arguments _dummyParsedArguments;
        private string[] _dummyArgs;

        private const string DUMMY_FILE_PATH = @"C:\SomeDummy\FilePath";
        private const string DUMMY_VERSION = "Dummy Version String";
        private const int DUMMY_SIZE = 1;

        [SetUp]
        public void Setup()
        {
            _mockArgumentsParser = MockRepository.GenerateMock<IArgumentsParser>();
            _mockFileDetailsWrapper = MockRepository.GenerateMock<IFileDetailsWrapper>();
            _stubConsoleWrapper = MockRepository.GenerateStub<IConsoleWrapper>();

            _dummyParsedArguments = new Arguments(OperationType.GetSize, DUMMY_FILE_PATH);
            _dummyArgs = new[] { string.Empty };

            _sut = new Application(_stubConsoleWrapper, _mockArgumentsParser, _mockFileDetailsWrapper);
        }

        [Test]
        public void calls_arguments_parser()
        {
            _mockArgumentsParser.Expect(m =>
                m.TryParseAgruments(Arg<string[]>.Is.Anything,
                out Arg<Arguments>.Out(_dummyParsedArguments).Dummy))
            .Return(true).Repeat.Once();

            _sut.Run(_dummyArgs);

            _mockArgumentsParser.VerifyAllExpectations();
        }

        [Test]
        public void does_not_execute_operation_if_arguments_not_parsed()
        {
            _mockArgumentsParser.Stub(m =>
                m.TryParseAgruments(Arg<string[]>.Is.Anything,
                out Arg<Arguments>.Out(_dummyParsedArguments).Dummy))
            .Return(false);

            _sut.Run(_dummyArgs);

            _mockFileDetailsWrapper.AssertWasNotCalled(m => m.Size(Arg<string>.Is.Anything));
        } 

        [Test]
        public void does_not_execute_operation_when_operation_type_invalid()
        {
            var dummyParsedArguments = new Arguments(OperationType.Invalid, DUMMY_FILE_PATH);

            _mockArgumentsParser.Stub(m =>
               m.TryParseAgruments(Arg<string[]>.Is.Anything,
               out Arg<Arguments>.Out(dummyParsedArguments).Dummy))
           .Return(true);

            _sut.Run(_dummyArgs);

            _mockFileDetailsWrapper.AssertWasNotCalled(m => m.Size(Arg<string>.Is.Anything));
        }

        [Test]
        public void calls_version_when_get_version_operation_type()
        {
            var dummyParsedArguments = new Arguments(OperationType.GetVersion, DUMMY_FILE_PATH);

            _mockArgumentsParser.Stub(m =>
               m.TryParseAgruments(Arg<string[]>.Is.Anything,
               out Arg<Arguments>.Out(dummyParsedArguments).Dummy))
           .Return(true);

            _mockFileDetailsWrapper.Expect(m => m.Version(Arg<string>.Is.Anything))
                .Return(DUMMY_VERSION).Repeat.Once();

            _mockFileDetailsWrapper.Expect(m => m.Size(Arg<string>.Is.Anything))
                .Return(DUMMY_SIZE).Repeat.Never();


            _sut.Run(_dummyArgs);

            _mockFileDetailsWrapper.VerifyAllExpectations();
        }

        [Test]
        public void calls_size_when_get_size_operation_type()
        {
            _mockArgumentsParser.Stub(m =>
               m.TryParseAgruments(Arg<string[]>.Is.Anything,
               out Arg<Arguments>.Out(_dummyParsedArguments).Dummy))
           .Return(true);

            _mockFileDetailsWrapper.Expect(m => m.Size(Arg<string>.Is.Anything))
                .Return(DUMMY_SIZE).Repeat.Once();

            _mockFileDetailsWrapper.Expect(m => m.Version(Arg<string>.Is.Anything))
                .Return(DUMMY_VERSION).Repeat.Never();

            _sut.Run(_dummyArgs);

            _mockFileDetailsWrapper.VerifyAllExpectations();
        }


    }
}
