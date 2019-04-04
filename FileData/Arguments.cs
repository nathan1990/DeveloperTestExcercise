using FileData.Operations;

namespace FileData
{
    public class Arguments
    {
        public Arguments(OperationType op, string filePath)
        {
            OperationToPerfom = op;
            FilePath = filePath;
        }

        public OperationType OperationToPerfom { get; private set; }

        public string FilePath { get; private set; }

        public bool AreValid()
        {
            return OperationToPerfom != OperationType.Invalid;
        }
    }
}
