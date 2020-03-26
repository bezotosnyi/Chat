namespace Chat.BLL.Services
{
    using System;
    using System.IO;

    using Chat.BLL.Infrastructure;

    using log4net;
    using log4net.Config;

    public class LoggingService : ILoggingService
    {
        //private static readonly Lazy<LoggingService> LazySingleton = new Lazy<LoggingService>(() => new LoggingService());

        private static readonly ILog Log = LogManager.GetLogger("ChatLogger");

        static LoggingService()
        {
            InitLogger();
        }

        /*private LoggingService()
        {
        }

        public static LoggingService Instance => LazySingleton.Value;*/

        public void Debug(object message)
        {
            Log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            Log.Debug(message, exception);
        }

        public void Error(object message)
        {
            Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Fatal(object message)
        {
            Log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void Info(object message)
        {
            Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            Log.Info(message, exception);
        }

        public void Warn(object message)
        {
            Log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            Log.Warn(message, exception);
        }

        private static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}
