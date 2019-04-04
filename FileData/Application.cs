using FileData.Interfaces;
using FileData.Operations;

namespace FileData
{
    public class Application : IApplication
    {
        private readonly IConsoleWrapper _consoleWrapper;
        private readonly IFileDetailsWrapper _fileDetailsWrapper;
        private readonly IArgumentsParser _argumentsParser;
        private const string OPERATION_PERFORMED_TEMPLATE = "Peformed OperationType : {0}, Result {1}";

        public Application(IConsoleWrapper consoleWrapper, IArgumentsParser argumentsParser, 
            IFileDetailsWrapper fileDetailsWrapper)
        {
            _consoleWrapper = consoleWrapper;
            _argumentsParser = argumentsParser;
            _fileDetailsWrapper = fileDetailsWrapper;
        }

        public void Run(string[] args)
        {
            Arguments parsedArgs;
            bool argumentsParsed = _argumentsParser.TryParseAgruments(args, out parsedArgs);

            if (!argumentsParsed)
            {
                HandleError("Unable to parse arguments please ensure only two agruments passed");
            }
            else if (!parsedArgs.AreValid())
            {
                HandleError("Invalid arguments - please ensure only supported operations are used");
            }
            else
            {
                PerformRequestedOperation(parsedArgs);
                _consoleWrapper.ReadKey(true);
            }
        }

        private void PerformRequestedOperation(Arguments arguments)
        {
            switch (arguments.OperationToPerfom)
            {
                case OperationType.GetSize:
                    var sizeResult = _fileDetailsWrapper.Size(arguments.FilePath);
                   _consoleWrapper.WriteLine(OPERATION_PERFORMED_TEMPLATE, arguments.OperationToPerfom, sizeResult);
                    break;
                case OperationType.GetVersion:
                    var versionResult = _fileDetailsWrapper.Version(arguments.FilePath);
                    _consoleWrapper.WriteLine(OPERATION_PERFORMED_TEMPLATE, arguments.OperationToPerfom, versionResult);
                    break;
            }
        }

        private void HandleError(string errorMessage)
        {
            _consoleWrapper.WriteLine(errorMessage);
            _consoleWrapper.ReadKey(true);
        }
    }
}
