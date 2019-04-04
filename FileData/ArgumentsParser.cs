using FileData.Interfaces;

namespace FileData
{
    public class ArgumentsParser : IArgumentsParser
    {
        private readonly IOperationTypeParser _operationsTypeParser;

        public ArgumentsParser(IOperationTypeParser operationsParser)
        {
            _operationsTypeParser = operationsParser;
        }

        public bool TryParseAgruments(string[] args, out Arguments parsedArguments)
        {
            parsedArguments = null;

            if(args.Length != 2)
            {
                return false;
            }

            var operationToPerform = _operationsTypeParser.Parse(args[0]);
            var filePath = args[1];

            parsedArguments = new Arguments(operationToPerform, filePath);
            return true;
        }
    }
}
