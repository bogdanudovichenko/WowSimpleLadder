using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WowSimpleLadder.Api.Interfaces;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.Enums;
using WowSimpleLadder.Models.Enums.Extensions;

namespace WowSimpleLadder.Api.Concrete
{
    public class WowApiClient : IWowApiClient
    {
        protected string BaseUrl { get; } = SimpleLadderConfig.WowLadderBaseUrl;
        protected string ApiKey { get; } = SimpleLadderConfig.WowApiKey;
        protected HttpClient HttpClient { get; } = new HttpClient();

        /// <summary>
        /// Return pvp world of warcraft ladder data from official blizzard web api for all local and brackets
        /// </summary>
        public async Task<IReadOnlyList<PvpApiRowModel>> GetAllPvpLadderRowsAsync()
        {
            var result = new List<PvpApiRowModel>();

            foreach (string localeName in Enum.GetNames(typeof(BlizzardLocale)))
            {
                Enum.TryParse(localeName, out BlizzardLocale blizzardLocale);

                if (blizzardLocale == BlizzardLocale.All)
                {
                    continue;
                }

                foreach (string bracketName in Enum.GetNames(typeof(WowPvpBracket)))
                {
                    Enum.TryParse(bracketName, out WowPvpBracket wowPvpBracket);

                    if (wowPvpBracket == WowPvpBracket.All)
                    {
                        continue;
                    }

                    IEnumerable<PvpApiRowModel> pvpLadderRowsBuf = await GetPvpLadderRowsAsync(blizzardLocale, wowPvpBracket);

                    if (pvpLadderRowsBuf != null)
                    {
                        result.AddRange(pvpLadderRowsBuf);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Return pvp world of warcraft ladder data from official blizzard web api
        /// </summary>
        /// <param name="locale">BlizzardLocale.All is invalid param for this method</param>
        /// <param name="bracket">WowPvpBracket.All is invalid param for this method</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<PvpApiRowModel>> GetPvpLadderRowsAsync(BlizzardLocale locale, WowPvpBracket bracket)
        {
            if (locale == BlizzardLocale.All)
            {
                throw new InvalidEnumArgumentException(nameof(BlizzardLocale.All));
            }

            if (bracket == WowPvpBracket.All)
            {
                throw new InvalidEnumArgumentException(nameof(WowPvpBracket.All));
            }

            var url = $"{BaseUrl}/{bracket.Stringify()}?locale={locale}&apikey={ApiKey}";

            try
            {
                using (var content = await HttpClient.GetStreamAsync(url))
                using (var streamReader = new StreamReader(content))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var serializer = new JsonSerializer();
                    var rows = serializer.Deserialize<PvpApiRowsModel>(jsonTextReader);

                    foreach (var row in rows.Rows)
                    {
                        row.Locale = (byte)locale;
                        row.DownloadedOn = DateTime.Now;
                        row.Bracket = (byte)bracket;
                    }

                    return rows.Rows;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
