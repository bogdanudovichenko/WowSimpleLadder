using System;
using System.Linq;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.TestConsole
{
    class Program
    {
        static void Main()
        {
            //var wowPvpLadderDownloadScheduler = new WowPvpLadderDownloadScheduler();
            //wowPvpLadderDownloadScheduler.Start();

            var repo = new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection);
            var result = repo.Get(BlizzardLocale.en_GB, WowPvpBracket.TwoVs2);
            Console.WriteLine(result.Count());

            Console.ReadKey();
        }
    }
}
