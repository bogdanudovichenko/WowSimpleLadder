using System;
using System.Collections.Generic;
using System.Text;

namespace WowSimpleLadder.Models.ApiModels
{
    public class PvpApiRowModel
    {
        public uint Ranking { get; set; }
        public ushort Rating { get; set; }
        public string Name { get; set; }
        public string RealmName { get; set; }
        public byte RaceId { get; set; }
        public byte ClassId { get; set; }
        public ushort SpecId { get; set; }
        public ushort FactionId { get; set; }
        public byte GenderId { get; set; }
        public uint SeasonWins { get; set; }
        public uint SeasonLosses { get; set; }
        public uint WeeklyWins { get; set; }
        public uint WeeklyLosses { get; set; }
    }
}
