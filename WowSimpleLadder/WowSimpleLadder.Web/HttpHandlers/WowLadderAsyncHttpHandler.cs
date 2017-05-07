using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.DAL.Repositories.Interfaces;
using WowSimpleLadder.Models.ApiModels;

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
            IEnumerable<PvpApiRowModel> rows = await _wowLadderRepository.GetAsync();
        }

        public override void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }
}