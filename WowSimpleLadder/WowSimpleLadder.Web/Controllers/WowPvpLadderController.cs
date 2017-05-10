using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.DAL.Repositories.Interfaces;
using WowSimpleLadder.Models.ApiModels;
using WowSimpleLadder.Models.ApiModels.Extensions;
using WowSimpleLadder.Web.Models.QueryModels;
using WowSimpleLadder.Web.Routers;

namespace WowSimpleLadder.Web.Controllers
{
    public sealed class WowPvpLadderController : BaseController
    {
        private readonly IWowLadderRepository _wowLadderRepository;

        public WowPvpLadderController(AbstractRouter router, HttpContext httpContext, HttpTaskAsyncHandler httpHandler, IWowLadderRepository wowLadderRepository)
            : base(router, httpContext, httpHandler)
        {
            if (wowLadderRepository == null)
            {
                throw new ArgumentNullException(nameof(wowLadderRepository));
            }

            _wowLadderRepository = wowLadderRepository;
        }

        public async Task<string> GetPvpLadder(WowLadderQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new ArgumentNullException(nameof(queryModel));
            }

            IReadOnlyList<PvpApiRowModel> rows = await _wowLadderRepository.GetAsync(
                queryModel.Locale, 
                queryModel.PvpBracket, 
                queryModel.WowClass, 
                queryModel.WowSpecId);

            return rows.ToJson();
        }

        public override void Dispose()
        {
            _wowLadderRepository?.Dispose();
        }
    }
}