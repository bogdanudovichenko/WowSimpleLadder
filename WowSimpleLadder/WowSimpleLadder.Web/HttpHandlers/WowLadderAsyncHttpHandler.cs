using System;
using System.Threading.Tasks;
using System.Web;
using WowSimpleLadder.Web.Routers;

namespace WowSimpleLadder.Web.HttpHandlers
{
    public sealed class WowLadderAsyncHttpHandler : HttpTaskAsyncHandler
    {
        public override bool IsReusable => false;

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            await BaseRouter.CallControllerAsync(this, context);
        }

        public override void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }
}