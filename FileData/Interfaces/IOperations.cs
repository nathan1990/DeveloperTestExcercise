using FileData.Operations;

namespace FileData.Interfaces
{
    public interface IOperation
    {
        char Flag { get; }
        string Name { get; }
        OperationType TypeOfOperation { get; }
    }
}
