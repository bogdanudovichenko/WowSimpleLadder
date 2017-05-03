namespace WowSimpleLadder.Models.Enums.Extensions
{
    public static class WowPvpBracketExtensions
    {
        public static string Stringify(this WowPvpBracket wowPvpBracket)
        {
            switch (wowPvpBracket)
            {
                case WowPvpBracket.TwoVs2:
                    return "2v2";
                case WowPvpBracket.ThreeVs3:
                    return "3v3";
                case WowPvpBracket.FiveVs5:
                    return "5v5";
                case WowPvpBracket.Rbg:
                    return "rbg";
                default:
                    return "2v2";
            }
        }
    }
}