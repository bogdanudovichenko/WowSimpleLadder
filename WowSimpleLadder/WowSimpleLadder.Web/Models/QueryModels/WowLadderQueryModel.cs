using System;
using System.Collections.Generic;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.Web.Models.QueryModels
{
    public class WowLadderQueryModel
    {
        public BlizzardLocale Locale { get; set; } = BlizzardLocale.All;
        public WowPvpBracket PvpBracket { get; set; } = WowPvpBracket.All;
        public WowClass WowClass { get; set; } = WowClass.All;
        public WowSpec WowSpecId { get; set; } = WowSpec.All;

        public WowLadderQueryModel(IDictionary<string, string> parametrs)
        {
            if (parametrs.ContainsKey("locale"))
            {
                BlizzardLocale locale;
                Locale = Enum.TryParse(parametrs["locale"], out locale) ? locale : BlizzardLocale.All;
            }

            if (parametrs.ContainsKey("pvpbracket"))
            {
                WowPvpBracket pvpBracket;
                PvpBracket = Enum.TryParse(parametrs["pvpbracket"], out pvpBracket) ? pvpBracket : WowPvpBracket.All;
            }

            if (parametrs.ContainsKey("wowclass"))
            {
                WowClass wowClass;
                WowClass = Enum.TryParse(parametrs["wowclass"], out wowClass) ? wowClass : WowClass.All;
            }

            if (parametrs.ContainsKey("specid"))
            {
                WowSpec specId;
                WowSpecId = Enum.TryParse(parametrs["specid"], out specId) ? specId : WowSpec.All;
            }
        }
    }
}