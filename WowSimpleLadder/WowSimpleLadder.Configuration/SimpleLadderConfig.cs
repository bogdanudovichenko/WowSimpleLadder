using System;

namespace WowSimpleLadder.Configuration
{
    public static class SimpleLadderConfig
    {
        public static string WowLadderLiteDbConnection => $@"{AppDomain.CurrentDomain.BaseDirectory}Content\WowLadder.db";
        public static string WowApiKey => "s92e46uejpf5ez2eebcds28fcgjt7mt6";
        public static string WowLadderBaseUrl = "https://eu.api.battle.net/wow/leaderboard";
    }
}