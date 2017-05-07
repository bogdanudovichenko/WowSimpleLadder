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
        private readonly LiteDatabase _liteDb;

        public WowLadderLiteDbRepository(string connection)
        {
            _liteDbRepo = new LiteRepository(connection);
            _liteDb = new LiteDatabase(connection);
        }

        public Task<IEnumerable<PvpApiRowModel>> GetAsync(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100)
        {
            return Task.Run(() => Get(locale, bracket, wowClass, spec, skip, take));
        }

        public IEnumerable<PvpApiRowModel> Get(BlizzardLocale locale = BlizzardLocale.All, WowPvpBracket bracket = WowPvpBracket.All,
            WowClass wowClass = WowClass.All, WowSpec spec = WowSpec.All, uint skip = 0, uint take = 100)
        {
            var collection = _liteDb.GetCollection<PvpApiRowModel>();

            Query filterQuery = null;

            if (locale != BlizzardLocale.All && bracket != WowPvpBracket.All)
            {
                var localeFilterQuery = Query.EQ("Locale", new BsonValue((int)locale));
                var bracketFilterQuery = Query.EQ("Bracket", new BsonValue((int)bracket));
                filterQuery = Query.And(localeFilterQuery, bracketFilterQuery);
            }
            else if (locale != BlizzardLocale.All)
            {
                filterQuery = Query.EQ("Locale", new BsonValue((int)locale));
            }
            else if (bracket != WowPvpBracket.All)
            {
                filterQuery = Query.EQ("Bracket", new BsonValue((int) bracket));
            }
            else
            {
                filterQuery = Query.All();
            }

            var ratingOrderQuery = Query.All("Rating");
            var nameOrderQuery = Query.All("Name");
            var parentOrderQuery = Query.And(ratingOrderQuery, nameOrderQuery);

            var parentQuery = Query.And(filterQuery, parentOrderQuery);

            var result = collection.Find(parentQuery, (int)skip, (int)take);

            return result;
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
