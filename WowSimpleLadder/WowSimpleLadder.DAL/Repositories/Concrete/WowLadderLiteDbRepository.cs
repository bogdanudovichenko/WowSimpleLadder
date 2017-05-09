using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteDB;
using WowSimpleLadder.DAL.Repositories.Interfaces;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.DAL.Repositories.Concrete
{
    public class WowLadderLiteDbRepository : IWowLadderRepository
    {
        private readonly LiteRepository _liteDbRepo;

        public WowLadderLiteDbRepository(string connection)
        {
            _liteDbRepo = new LiteRepository(connection);
        }

        public Task<IReadOnlyList<PvpApiRowModel>> GetAsync(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100)
        {
            return Task.Run(() => Get(locale, bracket, wowClass, spec, skip, take));
        }

        public IReadOnlyList<PvpApiRowModel> Get(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100)
        {
            var query = _liteDbRepo.Query<PvpApiRowModel>();

            if (locale != BlizzardLocale.All)
            {
                query = query.Where(row => row.Locale == (byte)locale);
            }

            if (bracket != WowPvpBracket.All)
            {
                query = query.Where(row => row.Bracket == (byte)bracket);
            }

            if (wowClass != WowClass.All)
            {
                query = query.Where(row => row.ClassId == (byte)wowClass);
            }

            if (spec != WowSpec.All)
            {
                query = query.Where(row => row.SpecId == (ushort)spec);
            }

            query = query.Where(Query.All("Rating"))
                         .Where(Query.All("Name"))
                         .Skip((int)skip)
                         .Limit((int)take);

            IReadOnlyList<PvpApiRowModel> result = query.ToList();

            return result;
        }

        public bool IsDownloadedToday => _liteDbRepo.FirstOrDefault<PvpApiRowModel>()?.DownloadedOn.Date == DateTime.Today;

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
