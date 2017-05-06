using System;
using System.Configuration;
using WowSimpleLadder.BLL.Services.Concrete;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //var wowPvpLadderDownloadScheduler = new WowPvpLadderDownloadScheduler();
            //wowPvpLadderDownloadScheduler.Start();

            string conn = ConfigurationManager.ConnectionStrings["WowLadderDbConnection"].ToString();
            var repo = new WowLadderLiteDbRepository(conn);
            var result = repo.Get(BlizzardLocale.en_GB, WowPvpBracket.TwoVs2);

            Console.ReadKey();
        }
    }
}
