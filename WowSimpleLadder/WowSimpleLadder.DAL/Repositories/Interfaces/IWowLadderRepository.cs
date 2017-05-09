using System.Collections.Generic;
using System.Threading.Tasks;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.DAL.Repositories.Interfaces
{
    public interface IWowLadderRepository
    {
        Task<IReadOnlyList<PvpApiRowModel>> GetAsync(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100);

        IReadOnlyList<PvpApiRowModel> Get(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All, 
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100);

        Task CreateAsync(IEnumerable<PvpApiRowModel> ladderRows);
        void Create(IEnumerable<PvpApiRowModel> ladderRows);

        void RemoveAllRecords();
        Task RemoveAllRecordsAsync();
        bool IsDownloadedToday { get; }
    }
}