using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.DAL.Repositories.Interfaces;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.ApiModels.Extensions;

namespace WowSimpleLadder.Web.HttpHandlers
{
    public sealed class WowLadderAsyncHttpHandler : HttpTaskAsyncHandler
    {
        private readonly IWowLadderRepository _wowLadderRepository;

        public override bool IsReusable => false;

        public WowLadderAsyncHttpHandler()
        {
            _wowLadderRepository = new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection);
        }

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            try
            {
                IEnumerable<PvpApiRowModel> rows = await _wowLadderRepository.GetAsync();
                string jsonResult = rows.ToJson();
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonResult);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Write("Internal Server Error: " + ex.Message);
            }
        }

        public override void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }
}