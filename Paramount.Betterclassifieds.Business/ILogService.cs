
using System;

namespace Paramount.Betterclassifieds.Business
{
    public interface ILogService
    {
        void Debug(string message);
        void Info(string message);
        void Info(string message, TimeSpan duration);
        void Warn(string message);
        void Warn(string message, TimeSpan duration);
        void Error(string message);
        void Error(string message, Exception exception);
        void Error(Exception exception);
    }
}
