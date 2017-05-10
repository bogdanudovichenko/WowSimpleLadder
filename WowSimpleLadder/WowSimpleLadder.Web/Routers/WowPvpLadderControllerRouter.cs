using System;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.Web.Controllers;
using WowSimpleLadder.Web.Models.QueryModels;

namespace WowSimpleLadder.Web.Routers
{
    public sealed class WowPvpLadderControllerRouter : AbstractRouter
    {
        private readonly WowPvpLadderController _wowPvpLadderController;

        public WowPvpLadderControllerRouter(HttpContext httpContext, HttpTaskAsyncHandler httpHandler)
            : base(httpContext, httpHandler)
        {
            _wowPvpLadderController = new WowPvpLadderController(this, HttpContext, HttpHandler,
                new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection));
        }

        public override async Task ExecuteAsync(string methodName)
        {
            if (methodName.ToLower() == "getpvpladder")
            {
                await ExecuteGetPvpLadderMethodAsync();
            }
            else
            {
                throw new Exception($"Can`t find method with name '{methodName}' in '{nameof(WowPvpLadderController)}'");
            }
        }

        // api/WowPvpLadderController/getpvpladder/
        private async Task ExecuteGetPvpLadderMethodAsync()
        {
            var queryModel = new WowLadderQueryModel(QueryStringParsed);
            string jsonResult = await _wowPvpLadderController.GetPvpLadder(queryModel);
            SendJsonResult(jsonResult);
        }

        private void SendJsonResult(string jsonResult)
        {
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.Write(jsonResult);
        }

        public override void Dispose()
        {
            _wowPvpLadderController?.Dispose();
        }
    }
}