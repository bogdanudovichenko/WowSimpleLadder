using System;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Configuration;
using WowSimpleLadder.DAL.Repositories.Concrete;
using WowSimpleLadder.Web.Controllers;

namespace WowSimpleLadder.Web.Routers
{
    public sealed class WowPvpLadderControllerRouter : AbstractRouter
    {
        private readonly WowPvpLadderController _wowPvpLadderController;

        public WowPvpLadderControllerRouter(HttpContext httpContext, HttpTaskAsyncHandler httpHandler)
            : base(httpContext, httpHandler)
        {
            _wowPvpLadderController = new WowPvpLadderController(HttpContext, HttpHandler,
                new WowLadderLiteDbRepository(SimpleLadderConfig.WowLadderLiteDbConnection));
        }

        public override async Task ExecuteAsync(string methodName)
        {
            if (methodName.ToLower() == "getpvpladder")
            {
                string jsonResult = await _wowPvpLadderController.GetPvpLadder();
                HttpContext.Response.ContentType = "application/json";
                HttpContext.Response.Write(jsonResult);
            }
            else
            {
                throw new Exception($"Can`t find method with name '{methodName}' in '{nameof(WowPvpLadderController)}'");
            }
        }

        public override void Dispose()
        {
            _wowPvpLadderController?.Dispose();
        }
    }
}