using System;
using System.Collections.Generic;
using System.Web;
using WowSimpleLadder.Web.Helpers;
using WowSimpleLadder.Web.Routers;

namespace WowSimpleLadder.Web.Controllers
{
    public abstract class BaseController : IDisposable
    {
        protected HttpContext HttpContext { get; }
        protected HttpTaskAsyncHandler HttpHandler { get; }
        protected HttpRequest HttpRequest { get; }
        protected HttpResponse HttpResponse { get; }
        protected Uri Url { get; }
        protected AbstractRouter Router { get; }

        protected IDictionary<string, string> QueryStringParsed { get; }

        protected BaseController(AbstractRouter router, HttpContext httpContext, HttpTaskAsyncHandler httpHandler)
        {
            if (router == null)
            {
                throw new ArgumentNullException(nameof(router));
            }

            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpHandler == null)
            {
                throw new ArgumentNullException(nameof(httpHandler));
            }

            HttpContext = httpContext;
            HttpHandler = httpHandler;
            HttpRequest = httpContext.Request;
            HttpResponse = httpContext.Response;

            Url = HttpRequest.Url;

            Router = router;
            QueryStringParsed = Router.QueryStringParsed;
        }

        public abstract void Dispose();
    }
}