﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;

namespace WowSimpleLadder.Api.Interfaces
{
    public interface IWowApiClient
    {
        Task<IReadOnlyList<PvpApiRowModel>> GetAllPvpLadderRowsAsync();
        Task<IReadOnlyList<PvpApiRowModel>> GetPvpLadderRowsAsync(BlizzardLocale locale, WowPvpBracket bracket);
    }
}
