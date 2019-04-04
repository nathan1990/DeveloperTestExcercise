using FileData.Interfaces;
using ThirdPartyTools;

namespace FileData
{
    public class FileDetailsWrapper : IFileDetailsWrapper
    {
        private FileDetails _fileDetails;

        public FileDetailsWrapper()
        {
            _fileDetails = new FileDetails();
        }

        public int Size(string filePath)
        {
            return _fileDetails.Size(filePath);
        }

        public string Version(string filePath)
        {
            return _fileDetails.Version(filePath);
        }
    }
}
