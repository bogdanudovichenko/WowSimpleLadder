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
                case WowPvpBracket.Rbg:
                    return "rbg";
                default:
                    return "2v2";
            }
        }

        public static string ParseBracketName(this WowPvpBracket wowPvpBracket, string bracketName)
        {
            if (bracketName == WowPvpBracket.TwoVs2.ToString())
            {
                return "2v2";
            }
            if (bracketName == WowPvpBracket.ThreeVs3.ToString())
            {
                return "3v3";
            }
            if (bracketName == WowPvpBracket.Rbg.ToString())
            {
                return "rbg";
            }

            return "2v2";
        }
    }
}