using FileData.Operations;

namespace FileData.Interfaces
{
    public interface IOperationTypeParser
    {
        OperationType Parse(string operationFlag);
    }
}
