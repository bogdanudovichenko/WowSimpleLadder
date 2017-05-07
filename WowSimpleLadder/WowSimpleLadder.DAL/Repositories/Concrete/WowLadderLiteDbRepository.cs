using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using WowSimpleLadder.DAL.Repositories.Interfaces;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.DAL.Repositories.Concrete
{
    public class WowLadderLiteDbRepository : IWowLadderRepository, IDisposable
    {
        private readonly LiteRepository _liteDbRepo;

        public WowLadderLiteDbRepository(string connection)
        {
            _liteDbRepo = new LiteRepository(connection);
        }

        public Task<IEnumerable<PvpApiRowModel>> GetAsync(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100)
        {
            return Task.Run(() => Get(locale, bracket, wowClass, spec, skip, take));
        }

        public IEnumerable<PvpApiRowModel> Get(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All, 
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100)
        {
            var rows = _liteDbRepo.Query<PvpApiRowModel>()
                .Where(row => row.Locale == (byte)locale && row.Bracket == (byte)bracket)
                .Skip((int)skip)
                .Limit((int)take)
                .ToList();

            rows = rows.OrderBy(row => row.Rating)
                .ThenBy(row => row.Ranking)
                .ThenBy(row => row.Name)
                .ThenBy(row => row.RealmName)
                .ToList();

            return rows;
        }

        public Task CreateAsync(IEnumerable<PvpApiRowModel> ladderRows)
        {
            return Task.Run(() => Create(ladderRows));
        }

        public void Create(IEnumerable<PvpApiRowModel> ladderRows)
        {
            if (ladderRows == null)
            {
                throw new ArgumentNullException(nameof(ladderRows));
            }

            foreach (PvpApiRowModel row in ladderRows)
            {
                using (var trans = _liteDbRepo.BeginTrans())
                {
                    _liteDbRepo.Insert(row);
                    trans.Commit();
                }
            }
        }

        public Task RemoveAllRecordsAsync()
        {
            return Task.Run(() => RemoveAllRecords());
        }

        public void RemoveAllRecords()
        {
            _liteDbRepo.Delete<PvpApiRowModel>(row => row.Id > 0);
        }

        public void Dispose()
        {
            _liteDbRepo?.Dispose();
        }
    }
}
