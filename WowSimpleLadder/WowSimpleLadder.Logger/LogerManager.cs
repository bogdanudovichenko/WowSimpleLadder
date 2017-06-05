using System;
using Serilog;
using WowSimpleLadder.Configuration;

namespace WowSimpleLadder.Logger
{
    public static class LogerManager
    {
        private static readonly Serilog.Core.Logger Logger;

        static LogerManager()
        {
            Logger = new LoggerConfiguration()
                .WriteTo.RollingFile(SimpleLadderConfig.LogFolder + "\\errors-{Date}.txt")
                .CreateLogger();
        }

        public static void LogError(Exception ex, string message = "")
        {
            Logger.Error(ex, message);
        }
    }
}
