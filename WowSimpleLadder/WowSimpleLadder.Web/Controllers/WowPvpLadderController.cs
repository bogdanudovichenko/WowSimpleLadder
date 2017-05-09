using System;
using System.Web;

namespace WowSimpleLadder.Web.Controllers
{
    public class WowPvpLadderController : BaseController
    { 
        public WowPvpLadderController(HttpContext httpContext, HttpTaskAsyncHandler httpHandler) : base(httpContext, httpHandler)
        {
        }

        public string GetPvpLadder()
        {
            throw new NotImplementedException();
        }
    }
}