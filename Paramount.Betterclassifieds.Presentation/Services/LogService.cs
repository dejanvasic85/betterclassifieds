using System;
using log4net;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class LogService : ILogService
    {
        private readonly ILog _logger;

        public LogService()
        {
            _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(Exception exception)
        {
            if (exception == null)
                return;

            _logger.Error(exception.Message, exception);
        }
    }
}