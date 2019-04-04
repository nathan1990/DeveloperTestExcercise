using FileData.Interfaces;

namespace FileData.Operations
{
    public class GetVersionOperation : IOperation
    {
        public char Flag => 'v';

        public string Name => "version";

        public OperationType TypeOfOperation => OperationType.GetVersion;
    }
}
