using FileData.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FileData.Operations
{
    public class OperationTypeParser : IOperationTypeParser
    {
        private const string OPERATION_TYPE_REGEX_TEMPLATE = @"^(([-]{{1,2}}|[\/]){0}$|[-]{{2}}{1}$)";
        private readonly IEnumerable<IOperation> _availableOperations;
        private TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(50);

        public OperationTypeParser(IEnumerable<IOperation> operations)
        {
            _availableOperations = operations;
        }

        public OperationType Parse(string operationFlag)
        {
            foreach(var operation in _availableOperations)
            {
                var operationMatchRegex = string.Format(OPERATION_TYPE_REGEX_TEMPLATE, operation.Flag, operation.Name);
                var operationRegex = new Regex(operationMatchRegex, RegexOptions.Singleline, _regexTimeout);

                if (operationRegex.IsMatch(operationFlag))
                {
                    return operation.TypeOfOperation;
                }
            }

            return OperationType.Invalid;
        }
    }
}
