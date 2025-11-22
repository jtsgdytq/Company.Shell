using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Logger
{
    public static class Logs
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void LogInfo(string message)
        {
            logger.Info(message);
        }

        public static void LogWarning(string message)
        {
            logger.Warn(message);
        }

        public static void LogDebug(string message)
        {
            logger.Debug(message);
        }


        public static void LogTrace(string message)
        {
            logger.Trace(message);
        }

        public static void LogFatal(string message)
        {
            logger.Fatal(message);
        }

        public static void LogError(Exception ex)
        {
            logger.Error(ex);
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
            {
                logger.Error(ex, message);
            }
            else
            {
                logger.Error(message);
            }
        }
    }
}
