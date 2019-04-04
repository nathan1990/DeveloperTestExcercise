namespace FileData.Interfaces
{
    public interface IArgumentsParser
    {
        bool TryParseAgruments(string[] args, out Arguments parsedArguments);
    }
}
