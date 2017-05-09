﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using WowSimpleLadder.Api.Concrete;
using WowSimpleLadder.Api.Interfaces;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.Models.ApiModels;

namespace WowSimpleLadder.BLL.QuartzJobs
{
    public sealed class WowLadderDownloadJob : IJob
    {
        private readonly IWowApiClient _wowApiClient = new WowApiClient();

        public void Execute(IJobExecutionContext context)
        {
            ExecuteAsync().Wait();
        }

        private async Task ExecuteAsync()
        {
            try
            {
                using (var repository = new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection))
                {
                    if (repository.IsDownloadedToday)
                    {
                        return;
                    }
                }

                IEnumerable<PvpApiRowModel> ladderRows = await _wowApiClient.GetAllPvpLadderRowsAsync();

                if (!ladderRows.Any())
                {
                    return;
                }

                using (var wowLadderRepository = new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection))
                {
                    await wowLadderRepository.RemoveAllRecordsAsync();
                    await wowLadderRepository.CreateAsync(ladderRows);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}