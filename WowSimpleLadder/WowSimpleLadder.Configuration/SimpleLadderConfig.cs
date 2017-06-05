using System;

namespace WowSimpleLadder.Configuration
{
    public static class SimpleLadderConfig
    {
        private static string BaseProjectPath => AppDomain.CurrentDomain.BaseDirectory;
        public static string WowLadderLiteDbConnection => $@"{BaseProjectPath}Content\WowLadder.db";
        public static string LogFolder => $@"{BaseProjectPath}Logs\";
        public static string WowApiKey => "s92e46uejpf5ez2eebcds28fcgjt7mt6";
        public static string WowLadderBaseUrl = "https://eu.api.battle.net/wow/leaderboard";
    }
}