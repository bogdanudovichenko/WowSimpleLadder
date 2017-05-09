using System;
using System.Web;

namespace WowSimpleLadder.Web.Routers
{
    public static class BaseRouter
    {
        public static void CallController(HttpTaskAsyncHandler httpHandler, HttpContext httpContext)
        {
            Uri url = httpContext.Request.Url;
            throw new NotImplementedException();
        }
    }
}