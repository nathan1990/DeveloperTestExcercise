using System;

namespace FileData.Interfaces
{
    public interface IConsoleWrapper
    {
        ConsoleKeyInfo ReadKey(bool intercept);
        void WriteLine(string format, params object[] arg);
    }
}
