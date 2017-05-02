using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowSimpleLadder.Api.Concrete;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var wowClient = new WowApiClient();

            var task = wowClient.GetPvpLadderRowsAsync(BlizzardLocale.en_GB, WowPvpBracket.TwoVs2);
            task.Wait();
            var result = task.Result;

            var specs = result.Select(row => row.SpecId).ToList();

            Console.ReadKey();
        }
    }
}
