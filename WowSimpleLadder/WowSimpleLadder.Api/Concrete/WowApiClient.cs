using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WowSimpleLadder.Api.Interfaces;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;
using WowSimpleLadder.Models.Enums.Extensions;

namespace WowSimpleLadder.Api.Concrete
{
    public class WowApiClient : IWowApiClient
    {
        protected string BaseUrl { get; } = "https://eu.api.battle.net/wow/leaderboard";
        protected string ApiKey { get; } = "s92e46uejpf5ez2eebcds28fcgjt7mt6";

        protected HttpClient HttpClient { get; } = new HttpClient();

        public async Task<IEnumerable<PvpApiRowModel>> GetPvpLadderRowsAsync(BlizzardLocale locale, WowPvpBracket bracket)
        {
            var url = $"{BaseUrl}/{bracket.Stringify()}?locale={locale.ToString()}&apikey={ApiKey}";

            using(var content = await HttpClient.GetStreamAsync(url))
            using (var streamReader = new StreamReader(content))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                var rows = serializer.Deserialize<PvpApiRowsModel>(jsonTextReader);
                return rows.Rows;
            }
        }
    }
}
