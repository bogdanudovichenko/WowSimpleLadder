using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using WowSimpleLadder.Api.Concrete;
using WowSimpleLadder.Api.Interfaces;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.DAL.Repositories.Interfaces;
using WowSimpleLadder.Models.ApiModels;

namespace WowSimpleLadder.BLL.QuartzJobs
{
    public sealed class WowLadderDownloadJob : IJob
    {
        private readonly IWowApiClient _wowApiClient = new WowApiClient();
        private readonly IWowLadderRepository _wowLadderRepository;

        public WowLadderDownloadJob()
        {
            _wowLadderRepository = new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection);
        }

        public void Execute(IJobExecutionContext context)
        {
            ExecuteAsync().Wait();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                IEnumerable<PvpApiRowModel> ladderRows = await _wowApiClient.GetAllPvpLadderRowsAsync();
                var pvpApiRowModelsArray = ladderRows as PvpApiRowModel[] ?? ladderRows.ToArray();

                if (pvpApiRowModelsArray.Any())
                {
                    await _wowLadderRepository.RemoveAllRecordsAsync();
                    await _wowLadderRepository.CreateAsync(pvpApiRowModelsArray);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}