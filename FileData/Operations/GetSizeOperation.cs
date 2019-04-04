using FileData.Interfaces;

namespace FileData.Operations
{
    public class GetSizeOperation : IOperation
    {
        public char Flag => 's';

        public string Name => "size";

        public OperationType TypeOfOperation => OperationType.GetSize;
    }
}
