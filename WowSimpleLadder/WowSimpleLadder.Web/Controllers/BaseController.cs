using System;
using System.Collections.Generic;
using System.Web;
using WowSimpleLadder.Web.Helpers;

namespace WowSimpleLadder.Web.Controllers
{
    public abstract class BaseController : IDisposable
    {
        protected HttpContext HttpContext { get; }
        protected HttpTaskAsyncHandler HttpHandler { get; }
        protected HttpRequest HttpRequest { get; }
        protected HttpResponse HttpResponse { get; }
        protected Uri Url { get; }
        protected IDictionary<string, string> QueryStringParsed { get; }

        protected BaseController(HttpContext httpContext, HttpTaskAsyncHandler httpHandler)
        {
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

            QueryStringParsed = QueryStringParser.Parse(Url.Query);
        }

        public abstract void Dispose();
    }
}