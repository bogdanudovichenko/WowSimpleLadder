using System.Collections.Generic;
using System.Threading.Tasks;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.DAL.Repositories.Interfaces
{
    public interface IWowLadderRepository
    {
        Task<IEnumerable<PvpApiRowModel>> GetAsync(BlizzardLocale locale, WowPvpBracket bracket,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100);

        IEnumerable<PvpApiRowModel> Get(BlizzardLocale locale, WowPvpBracket bracket, 
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100);

        Task CreateAsync(IEnumerable<PvpApiRowModel> ladderRows);
        void Create(IEnumerable<PvpApiRowModel> ladderRows);

        void RemoveAllRecords();
        Task RemoveAllRecordsAsync();
    }
}