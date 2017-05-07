using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WowSimpleLadder.Models.ApiModels.Extensions
{
    public static class PvpApiRowModelExtensions
    {
        /// <summary>
        /// Manually json serialization.
        /// </summary>
        /// <param name="pvpApiRowModel">Cannot be null.</param>
        /// <returns>Serialized json string.</returns>
        public static string ToJson(this PvpApiRowModel pvpApiRowModel)
        {
            if (pvpApiRowModel == null)
            {
                throw new ArgumentNullException(nameof(pvpApiRowModel));
            }

            using (var stringWriter = new StringWriter())
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
                jsonTextWriter.WriteStartObject();
                //{
                jsonTextWriter.WritePropertyName("id");
                jsonTextWriter.WriteValue(pvpApiRowModel.Id);

                jsonTextWriter.WritePropertyName("ranking");
                jsonTextWriter.WriteValue(pvpApiRowModel.Ranking);

                jsonTextWriter.WritePropertyName("rating");
                jsonTextWriter.WriteValue(pvpApiRowModel.Rating);

                jsonTextWriter.WritePropertyName("name");
                jsonTextWriter.WriteValue(pvpApiRowModel.Name);

                jsonTextWriter.WritePropertyName("realmName");
                jsonTextWriter.WriteValue(pvpApiRowModel.RealmName);

                jsonTextWriter.WritePropertyName("raceId");
                jsonTextWriter.WriteValue(pvpApiRowModel.RaceId);

                jsonTextWriter.WritePropertyName("classId");
                jsonTextWriter.WriteValue(pvpApiRowModel.ClassId);

                jsonTextWriter.WritePropertyName("specId");
                jsonTextWriter.WriteValue(pvpApiRowModel.SpecId);

                jsonTextWriter.WritePropertyName("factionId");
                jsonTextWriter.WriteValue(pvpApiRowModel.FactionId);

                jsonTextWriter.WritePropertyName("genderId");
                jsonTextWriter.WriteValue(pvpApiRowModel.GenderId);

                jsonTextWriter.WritePropertyName("seasonWins");
                jsonTextWriter.WriteValue(pvpApiRowModel.SeasonWins);

                jsonTextWriter.WritePropertyName("seasonLosses");
                jsonTextWriter.WriteValue(pvpApiRowModel.SeasonLosses);

                jsonTextWriter.WritePropertyName("weeklyWins");
                jsonTextWriter.WriteValue(pvpApiRowModel.WeeklyWins);

                jsonTextWriter.WritePropertyName("weeklyLosses");
                jsonTextWriter.WriteValue(pvpApiRowModel.WeeklyLosses);

                jsonTextWriter.WritePropertyName("Bracket");
                jsonTextWriter.WriteValue(pvpApiRowModel.Bracket);

                jsonTextWriter.WritePropertyName("locale");
                jsonTextWriter.WriteValue(pvpApiRowModel.Locale);
                //}
                jsonTextWriter.WriteEndObject();

                return stringWriter.ToString();
            }
        }

        public static string ToJson(this IEnumerable<PvpApiRowModel> pvpApiRowModels)
        {
            if (pvpApiRowModels == null)
            {
                throw new ArgumentNullException(nameof(pvpApiRowModels));
            }

            using (var stringWriter = new StringWriter())
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
                jsonTextWriter.WriteStartArray();
                //[
                foreach (PvpApiRowModel pvpApiRowModel in pvpApiRowModels)
                {
                    if (pvpApiRowModel != null)
                    {
                        jsonTextWriter.WriteValue(pvpApiRowModel.ToJson());
                    }
                }
                //]
                jsonTextWriter.WriteEndArray();
                return stringWriter.ToString();
            }
        }
    }
}