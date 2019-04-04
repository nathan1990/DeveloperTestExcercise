namespace FileData.Interfaces
{
    public interface IFileDetailsWrapper
    {
        string Version(string filePath);
        int Size(string filePath);
    }
}
