using FileData.Interfaces;
using FileData.Operations;
using NUnit.Framework;
using System.Collections.Generic;

namespace FileData.Tests
{
    [TestFixture]
    public class OptionTypeParserTests
    {
        private OperationTypeParser _sut;

        private const string TEST_OPERATION_NAME = "test";
        private const char TEST_OPERATION_FLAG = 't';

        [SetUp]
        public void Setup()
        {
            _sut = new OperationTypeParser(new List<IOperation> { new TestOption() });
        }

        [Test]
        [TestCaseSource("OperationParsingTestCases")]
        public void parses_operations_correctly(string operationFlag, OperationType expectedOperationType)
        {
            var actualOperationType = _sut.Parse(operationFlag);

            Assert.AreEqual(expectedOperationType, actualOperationType);
        }

        public static IEnumerable<TestCaseData> OperationParsingTestCases
        {
            get
            {
                yield return new TestCaseData("-t", OperationType.GetVersion).SetName("correctly_parses_single_hyphen_flag");
                yield return new TestCaseData("--t", OperationType.GetVersion).SetName("correctly_parses_double_hyphen_flag");
                yield return new TestCaseData("/t", OperationType.GetVersion).SetName("correctly_parses_forward_slash_flag");
                yield return new TestCaseData("--test", OperationType.GetVersion).SetName("correctly_parses_double_hyphen_name_flag");
                yield return new TestCaseData("-test", OperationType.Invalid).SetName("correctly_parses_single_hyphen_name_flag");
                yield return new TestCaseData("/test", OperationType.Invalid).SetName("correctly_parses_forward_slash_name_flag");
                yield return new TestCaseData("a-t", OperationType.Invalid).SetName("correctly_parses_ending_single_hyphen_flag");
                yield return new TestCaseData("*t", OperationType.Invalid).SetName("correctly_parses_star_flag");
                yield return new TestCaseData("-x", OperationType.Invalid).SetName("correctly_parses_unrecognized_flag");
            }
        }

        public class TestOption : IOperation
        {
            public char Flag => TEST_OPERATION_FLAG;

            public string Name => TEST_OPERATION_NAME;

            public OperationType TypeOfOperation => OperationType.GetVersion;
        }
    }
}
